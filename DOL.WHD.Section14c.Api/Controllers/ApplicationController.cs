using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Factories;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Log.LogHelper;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Generic;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using DOL.WHD.Section14c.Business.Helper;
using DOL.WHD.Section14c.Common;
using DOL.WHD.Section14c.EmailApi.Helper;
using System.Web.Http.Results;

namespace DOL.WHD.Section14c.Api.Controllers
{
    /// <summary>
    /// Operations on a submitted application
    /// </summary>
    [AuthorizeHttps]
    [RoutePrefix("api/application")]
    public class ApplicationController : BaseApiController
    {
        private readonly IIdentityService _identityService;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationSubmissionValidator _applicationSubmissionValidator;
        private readonly IApplicationSummaryFactory _applicationSummaryFactory;
        private readonly IStatusService _statusService;
        private readonly ISaveService _saveService;
        private readonly IAttachmentService _attachmentService;
        private readonly IEmailService _emailService;
        /// <summary>
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="identityService"></param>
        /// <param name="applicationService"></param>
        /// <param name="applicationSubmissionValidator"></param>
        /// <param name="applicationSummaryFactory"></param>
        /// <param name="statusService"></param>
        /// <param name="saveService"></param>
        /// <param name="attachmentService"></param>
        /// <param name="emailService"></param>
        public ApplicationController(IIdentityService identityService, IApplicationService applicationService, IApplicationSubmissionValidator applicationSubmissionValidator, IApplicationSummaryFactory applicationSummaryFactory, IStatusService statusService, ISaveService saveService, IAttachmentService attachmentService, IEmailService emailService)
        {
            _identityService = identityService;
            _applicationService = applicationService;
            _applicationSubmissionValidator = applicationSubmissionValidator;
            _applicationSummaryFactory = applicationSummaryFactory;
            _statusService = statusService;
            _saveService = saveService;
            _attachmentService = attachmentService;
            _emailService = emailService;
        }

        /// <summary>
        /// Submit 14c application
        /// </summary>
        /// <returns>Http status code</returns>
        [HttpPost]
        [Route("submit")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public async Task<IHttpActionResult> Submit([FromBody]ApplicationSubmission submission)
        {
            var results = _applicationSubmissionValidator.Validate(submission);
            if (!results.IsValid)
            {
                BadRequest(results.Errors.ToString());
            }

            _applicationService.ProcessModel(submission);

            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, submission.EIN);
            if (!hasEINClaim)
            {
                Unauthorized("Unauthorized");
            }

            await _applicationService.SubmitApplicationAsync(submission);

            // remove the associated application save
            _saveService.Remove(submission.EIN);

            var response = await GetApplicationDocument(new Guid(submission.Id));

            // Get return value from API call
            var contentResult = response as OkNegotiatedContentResult<byte[]>;
            var returnValue = contentResult.Content;

            if (returnValue == null)
            {
                InternalServerError("Get concatenate Pdf failed");
            }

            // Calling Concatenate Web API
            var baseUri = new Uri(AppSettings.Get<string>("EmailApiBaseUrl"));
            var httpClientConnectionLeaseTimeout = AppSettings.Get<int>("HttpClientConnectionLeaseTimeout");
            // Get Http Client
            var httpClientInstance = MyHttpClient;
            httpClientInstance.DefaultRequestHeaders.Clear();
            httpClientInstance.DefaultRequestHeaders.ConnectionClose = false;
            httpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
            if (httpClientInstance.BaseAddress != baseUri)
                httpClientInstance.BaseAddress = baseUri;
            ServicePointManager.FindServicePoint(baseUri).ConnectionLeaseTimeout = httpClientConnectionLeaseTimeout;

            // Get Email Contents
            var emailTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/EmailTemplate.html");
            var emailTemplateString = File.ReadAllText(emailTemplatePath);
            var emailContents = _emailService.PrepareApplicationEmailContents(submission, emailTemplateString, EmailReceiver.Both);
            // Call Document Management Web API
            foreach (var content in emailContents)
            {
                content.Value.attachments = new Dictionary<string, byte[]>() { { "Concatenate.pdf", returnValue } };
                await httpClientInstance.PostAsJsonAsync<EmailContent>("/api/email/sendemail", content.Value);
            }

            return Ok();
        }

        /// <summary>
        /// Returns 14c application by Id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [AuthorizeClaims(ApplicationClaimTypes.ViewAllApplications)]
        public IHttpActionResult GetApplication(Guid id)
        {
            var application = _applicationService.GetApplicationById(id);
            if (application == null)
            {
                NotFound("Application not found");
            }

            return Ok(application);
        }

        /// <summary>
        /// Gets summary collection of all 14c applications
        /// </summary>
        [HttpGet]
        [Route("summary")]
        [AuthorizeClaims(ApplicationClaimTypes.ViewAllApplications)]
        public IHttpActionResult GetApplicationsSummary()
        {
            var allApplications = _applicationService.GetAllApplications();
            var applicationSummaries = allApplications.Select(x => _applicationSummaryFactory.Build(x));
            return Ok(applicationSummaries);
        }

        /// <summary>
        /// Change application status
        /// </summary>
        /// <param name="id">Application Id</param>
        /// <param name="statusId">Status Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("status")]
        [AuthorizeClaims(ApplicationClaimTypes.ChangeApplicationStatus)]
        public async Task<IHttpActionResult> ChangeApplicationStatus(Guid id, int statusId)
        {
            var application = _applicationService.GetApplicationById(id);
            if (application == null)
            {
                NotFound("Application not found");
            }

            // check status id to make sure it is valid
            var status = _statusService.GetStatus(statusId);
            if (status == null)
            {
                BadRequest("Status Id is not valid");
            }

            await _applicationService.ChangeApplicationStatus(application, statusId);
            return Ok($"/api/application?id={id}");
        }

        /// <summary>
        /// Get Application Document
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="download"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("applicationdocument")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication, ApplicationClaimTypes.ViewAllApplications)]
        public async Task<IHttpActionResult> GetApplicationDocument(Guid applicationId)
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                // Get Application Template
                var applicationViewTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Section14cApplicationPdfView.html");

                ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_applicationService, _attachmentService);
                var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(applicationId, applicationViewTemplatePath);

                // Calling Concatenate Web API
                var baseUri = new Uri(AppSettings.Get<string>("PdfApiBaseUrl"));
                var httpClientConnectionLeaseTimeout = AppSettings.Get<int>("HttpClientConnectionLeaseTimeout");
                // Get Http Client
                var httpClientInstance = MyHttpClient;
                httpClientInstance.DefaultRequestHeaders.Clear();
                httpClientInstance.DefaultRequestHeaders.ConnectionClose = false;
                httpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                if (httpClientInstance.BaseAddress != baseUri)
                    httpClientInstance.BaseAddress = baseUri;
                ServicePointManager.FindServicePoint(baseUri).ConnectionLeaseTimeout = httpClientConnectionLeaseTimeout;

                // Call Document Management Web API
                var pdfGenerationResponse = await httpClientInstance.PostAsJsonAsync<List<PDFContentData>>("/api/documentmanagement/Concatenate", applicationAttachmentsData);

                // Get return value from API call
                var returnValue = await pdfGenerationResponse.Content.ReadAsAsync<byte[]>();

                if (returnValue == null)
                {
                    InternalServerError("Concatenate Pdf failed");
                }

                // This will return the pdf file byte array
                return Ok(returnValue);
            }
            catch (Exception ex)
            {
                if (ex is ObjectNotFoundException || ex is FileNotFoundException)
                {
                    NotFound("Not found");
                }

                InternalServerError(ex.Message);
            }
            // Replaceed Ok(Response) to fix the API response error
            // Return exceptions and custom messages
            return ResponseMessage(responseMessage);
        }

        /// <summary>
        /// Download Application Documents as a single pdf file
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("download")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication, ApplicationClaimTypes.ViewAllApplications)]
        public async Task<IHttpActionResult> DownloadApplicationDocument(Guid applicationId)
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var response = await GetApplicationDocument(applicationId);

                // Get return value from API call
                var contentResult = response as OkNegotiatedContentResult<byte[]>;
                var returnValue = contentResult.Content;

                if (returnValue == null)
                {
                    InternalServerError("Get concatenate Pdf failed");
                }
                // Download PDF File
                responseMessage = Download(returnValue, responseMessage, "Concatenate");
            }
            catch (Exception ex)
            {
                if (ex is ObjectNotFoundException || ex is FileNotFoundException)
                {
                    NotFound("Not found");
                }

                InternalServerError(ex.Message);
            }
            return ResponseMessage(responseMessage);
        }

        /// <summary>
        /// OPTIONS endpoint for CORS
        /// </summary>
        [AllowAnonymous]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}