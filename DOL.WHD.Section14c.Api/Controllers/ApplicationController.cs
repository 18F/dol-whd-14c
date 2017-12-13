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
using DOL.WHD.Section14c.DataAccess.Identity;
using Microsoft.AspNet.Identity.Owin;
using DOL.WHD.Section14c.Domain.Models;

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
        private readonly IEmailContentService _emailService;
        private readonly IOrganizationService _organizationService;
        private readonly IEmployerService _employerService;
        private readonly IResponseService _responseService;
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Gets the user manager for the controller
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        /// <summary>
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="identityService">
        /// The identity service this controller should use
        /// </param>
        /// <param name="applicationService">
        /// The application service this controller should use
        /// </param>
        /// <param name="applicationSubmissionValidator">
        /// The application submission validator this controller should use
        /// </param>
        /// <param name="applicationSummaryFactory">
        /// The application summary factory this controller should use
        /// </param>
        /// <param name="statusService">
        /// The status service this controller should use
        /// </param>
        /// <param name="saveService">
        /// The save service this controller should use
        /// </param>
        /// <param name="attachmentService">
        /// The attachment service this controller should use
        /// </param>
        /// <param name="emailService">
        /// The email service this controller should use
        /// </param>
        ///  /// <param name="organizationService">
        /// The organization service this controller should use
        /// </param>
        /// <param name="employerService">
        /// The employer service this controller should use
        /// </param>
        /// <param name="responseService">
        /// The response service this controller should use
        /// </param>
        public ApplicationController(IIdentityService identityService, IApplicationService applicationService, IApplicationSubmissionValidator applicationSubmissionValidator, IApplicationSummaryFactory applicationSummaryFactory, IStatusService statusService, ISaveService saveService, IAttachmentService attachmentService, IEmailContentService emailService, IOrganizationService organizationService, IEmployerService employerService, IResponseService responseService)
        {
            _identityService = identityService;
            _applicationService = applicationService;
            _applicationSubmissionValidator = applicationSubmissionValidator;
            _applicationSummaryFactory = applicationSummaryFactory;
            _statusService = statusService;
            _saveService = saveService;
            _attachmentService = attachmentService;
            _emailService = emailService;
            _organizationService = organizationService;
            _employerService = employerService;
            _responseService = responseService;
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
            AccountController account = new AccountController(_employerService, _organizationService);
            account.UserManager = UserManager;
            var userInfo = account.GetUserInfo();
            var results = _applicationSubmissionValidator.Validate(submission);
            if (!results.IsValid)
            {
                BadRequest(results.Errors.ToString());
            }

            _applicationService.ProcessModel(submission);

            // make sure user has rights to the Id
            var hasPermission = _identityService.HasSavePermission(userInfo, submission.Id);
            if (!hasPermission)
            {
                Unauthorized("Unauthorized");
            }

            await _applicationService.SubmitApplicationAsync(submission);

            var user = UserManager.Users.SingleOrDefault(s => s.Id == userInfo.UserId);
            user.Organizations.FirstOrDefault(x => x.ApplicationId == submission.Id).ApplicationStatusId = StatusIds.Submitted;
            await UserManager.UpdateAsync(user);

            // remove the associated application save
            _saveService.Remove(submission.Id);

            var response = await GetApplicationDocument(new Guid(submission.Id));

            // Get return value from API call
            var contentResult = response as OkNegotiatedContentResult<byte[]>;
            var returnValue = contentResult.Content;

            if (returnValue == null)
            {
                InternalServerError("Get concatenate Pdf failed");
            }

            // Calling Email Web API
            var baseUri = new Uri(AppSettings.Get<string>("EmailApiBaseUrl"));
            var httpClientConnectionLeaseTimeout = AppSettings.Get<int>("HttpClientConnectionLeaseTimeout");
            // Get Http Client
            var httpClientInstance = MyHttpClient;
            httpClientInstance.DefaultRequestHeaders.Clear();
            httpClientInstance.DefaultRequestHeaders.ConnectionClose = false;
            httpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (httpClientInstance.BaseAddress != baseUri)
                httpClientInstance.BaseAddress = baseUri;
            ServicePointManager.FindServicePoint(baseUri).ConnectionLeaseTimeout = httpClientConnectionLeaseTimeout;

            // Get Email Contents
            var certificationTeamEmailTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/CertificationTeamEmailTemplate.txt");
            var certificationTeamEmailTemplateString = File.ReadAllText(certificationTeamEmailTemplatePath);
            var employerEmailTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/EmployerEmailTemplate.txt");
            var employerEmailTemplateString = File.ReadAllText(employerEmailTemplatePath);
            var emailContents = _emailService.PrepareApplicationEmailContents(submission, certificationTeamEmailTemplateString, employerEmailTemplateString, EmailReceiver.Both);
            var pdfName = string.Format("14c_Application_{0}_{1}_{2}.pdf", submission.Employer.PhysicalAddress.State, DateTime.Now.ToString("yyyy-MM-dd"), Regex.Replace(submission.Employer.LegalName, @"\s+", "-"));
            // Call Document Management Web API
            foreach (var content in emailContents)
            {
                content.Value.Attachments = new Dictionary<string, byte[]>() { { pdfName, returnValue } };
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
        /// <param name="applicationId">
        /// Application GUID
        /// </param>
        /// <returns></returns>
        /// <summary>
        /// Get Application Document
        /// </summary>
        /// <param name="applicationId">
        /// Application GUID
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("applicationdocument")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication, ApplicationClaimTypes.ViewAllApplications)]
        public async Task<IHttpActionResult> GetApplicationDocument(Guid applicationId)
        {
            byte[] buffer = null;
            try
            {
                // Get Application Template
                var applicationTemplatesPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/HtmlTemplates");
                var templatefiles = Directory.GetFiles(applicationTemplatesPath, "*.html").OrderBy(f => new FileInfo(f).Name).ToList();

                ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_applicationService, _attachmentService, _responseService);
                var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(applicationId, templatefiles);

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
                buffer = await pdfGenerationResponse.Content.ReadAsAsync<byte[]>();

                if (buffer == null)
                {
                    InternalServerError("Concatenate Pdf failed");
                }
            }
            catch (Exception ex)
            {
                if (ex is ObjectNotFoundException || ex is FileNotFoundException)
                {
                    NotFound("Not found");
                }

                InternalServerError(ex.Message);
            }
            // This will return the pdf file byte array
            return Ok(buffer);
        }

        /// <summary>
        /// Download Application Documents as a single pdf file
        /// </summary>
        /// <param name="applicationId">
        /// Application GUID
        /// </param>
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
                var application = _applicationService.GetApplicationById(applicationId);                
                var pdfName = string.Format("14c_Application_{0}_{1}_{2}", application.Employer.PhysicalAddress.State, application.CreatedAt.ToString("yyyy-MM-dd"), Regex.Replace(application.Employer.LegalName, @"\s+", "-"));

                // Get return value from API call
                var contentResult = response as OkNegotiatedContentResult<byte[]>;
                var returnValue = contentResult.Content;

                if (returnValue == null)
                {
                    InternalServerError("Get concatenate Pdf failed");
                }
                // Download PDF File
                responseMessage = Download(returnValue, responseMessage, pdfName);
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