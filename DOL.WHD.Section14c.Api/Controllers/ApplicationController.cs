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

namespace DOL.WHD.Section14c.Api.Controllers
{
    /// <summary>
    /// Operations on a submitted application
    /// </summary>
    [AuthorizeHttps]
    [RoutePrefix("api/application")]
    public class ApplicationController : ApiController
    {
        private readonly IIdentityService _identityService;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationSubmissionValidator _applicationSubmissionValidator;
        private readonly IApplicationSummaryFactory _applicationSummaryFactory;
        private readonly IStatusService _statusService;
        private readonly ISaveService _saveService;
        /// <summary>
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="identityService"></param>
        /// <param name="applicationService"></param>
        /// <param name="applicationSubmissionValidator"></param>
        /// <param name="applicationSummaryFactory"></param>
        /// <param name="statusService"></param>
        /// <param name="saveService"></param>
        public ApplicationController(IIdentityService identityService, IApplicationService applicationService, IApplicationSubmissionValidator applicationSubmissionValidator, IApplicationSummaryFactory applicationSummaryFactory, IStatusService statusService, ISaveService saveService)
        {
            _identityService = identityService;
            _applicationService = applicationService;
            _applicationSubmissionValidator = applicationSubmissionValidator;
            _applicationSummaryFactory = applicationSummaryFactory;
            _statusService = statusService;
            _saveService = saveService;
        }

        /// <summary>
        /// Submit 14c application
        /// </summary>
        /// <returns>Http status code</returns>
        [HttpPost]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public async Task<HttpResponseMessage> Submit([FromBody]ApplicationSubmission submission)
        {
            var results = _applicationSubmissionValidator.Validate(submission);
            if (!results.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, results.Errors);
            }

            _applicationService.ProcessModel(submission);

            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, submission.EIN);
            if (!hasEINClaim)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            await _applicationService.SubmitApplicationAsync(submission);

            // remove the associated application save
            _saveService.Remove(submission.EIN);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Returns 14c application by Id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [AuthorizeClaims(ApplicationClaimTypes.ViewAllApplications)]
        public HttpResponseMessage GetApplication(Guid id)
        {
            var application = _applicationService.GetApplicationById(id);
            if (application != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, application);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Gets summary collection of all 14c applications
        /// </summary>
        [HttpGet]
        [Route("summary")]
        [AuthorizeClaims(ApplicationClaimTypes.ViewAllApplications)]
        public HttpResponseMessage GetApplicationsSummary()
        {
            var allApplications = _applicationService.GetAllApplications();
            var applicationSummaries = allApplications.Select(x => _applicationSummaryFactory.Build(x));
            return Request.CreateResponse(HttpStatusCode.OK, applicationSummaries);
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
        public async Task<HttpResponseMessage> ChangeApplicationStatus(Guid id, int statusId)
        {
            var application = _applicationService.GetApplicationById(id);
            if (application == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
            // check status id to make sure it is valid
            var status = _statusService.GetStatus(statusId);
            if (status == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Status Id is not valid");
            }

            await _applicationService.ChangeApplicationStatus(application, statusId);
            return Request.CreateResponse(HttpStatusCode.OK, $"/api/application?id={id}");
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