using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using DOL.WHD.Section14c.Log.LogHelper;
using DOL.WHD.Section14c.DataAccess.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Api.Controllers
{
    /// <summary>
    /// Controller to manage application save states
    /// </summary>
    [AuthorizeHttps]
    [RoutePrefix("api/save")]
    public class SaveController : BaseApiController
    {
        private readonly ISaveService _saveService;
        private readonly IIdentityService _identityService;
        private readonly IOrganizationService _organizationService;
        private readonly IEmployerService _employerService;
        private readonly IAttachmentService _attachmentService;
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
        /// Constructor to inject services
        /// </summary>
        /// <param name="saveService">The Save service this controller should use </param>
        /// <param name="identityService">The Identity service this controller should use </param>
        /// <param name="organizationService">The Organization service this controller should use </param>
        /// <param name="employerService">The Employer service this controller should use </param>
        /// <param name="attachmentService">The attachment service this controller should use </param>
        public SaveController(ISaveService saveService, IIdentityService identityService, IOrganizationService organizationService, IEmployerService employerService, IAttachmentService attachmentService)
        {
            _saveService = saveService;
            _identityService = identityService;
            _organizationService = organizationService;
            _employerService = employerService;
            _attachmentService = attachmentService;
        }

        /// <summary>
        /// Returns pre-submission 14c application
        /// </summary>
        /// <param name="applicationId">Application Identification Number</param>
        [HttpGet]
        [Route("{applicationId}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult GetSave(string applicationId)
        {
            AccountController account = new AccountController(_employerService, _organizationService);
            account.UserManager = UserManager;
            var userInfo = account.GetUserInfo();
            // make sure user has rights to the Applicaion
            var hasPermission = _identityService.HasSavePermission(userInfo, applicationId);
            if (!hasPermission)
            {
                Unauthorized("Unauthorized");
            }

            var applicationSave = _saveService.GetSave(applicationId);
            if (applicationSave == null)
            {
                NotFound("Application Not found");
            }

            return Ok(applicationSave.ApplicationState);

        }

        /// <summary>
        /// Creates or updates pre-submission 14c application
        /// </summary>
        /// <param name="employerId">Employer Identification Number</param>
        /// <param name="applicationId">Application Identification Number</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{employerId}/{applicationId}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult AddSave(string employerId, string applicationId)
        {
            AccountController account = new AccountController(_employerService, _organizationService);
            account.UserManager = UserManager;
            var userInfo = account.GetUserInfo();
            // make sure user has rights to the Applicaion
            var hasPermission = _identityService.HasAddPermission(userInfo, employerId);
            if (!hasPermission)
            {
                Unauthorized("Unauthorized");
            }

            var state = Request.Content.ReadAsStringAsync().Result;
            try
            {
                JToken.Parse(state);
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            var user = UserManager.Users.SingleOrDefault(s => s.Id == userInfo.UserId);
            var org = user.Organizations.FirstOrDefault(x => x.ApplicationId == applicationId);
            _saveService.AddOrUpdate(applicationId, applicationId, employerId, state);

            if (org.ApplicationStatusId == StatusIds.New)
            {
                // Update Organization Status
                org.ApplicationStatusId = StatusIds.InProgress;
                UserManager.UpdateAsync(user);
            }
            return Created($"/api/Save?userId={User.Identity.GetUserId()}", new { });
        }

        /// <summary>
        /// Creates or updates pre-submission 14c application
        /// </summary>
        /// <param name="applicationId">Application Identification Number</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateSave")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult UpdateSave(string applicationId)
        {
            AccountController account = new AccountController(_employerService, _organizationService);
            account.UserManager = UserManager;
            var userInfo = account.GetUserInfo();
            // make sure user has rights to the Applicaion
            var hasPermission = _identityService.HasSavePermission(userInfo, applicationId);
            if (!hasPermission)
            {
                Unauthorized("Unauthorized");
            }

            var state = Request.Content.ReadAsStringAsync().Result;
            try
            {
                JToken.Parse(state);
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            _saveService.AddOrUpdate(applicationId, applicationId, null, state);
            return Created($"/api/Save?userId={User.Identity.GetUserId()}", new { });
        }

        /// <summary>
        /// Removes pre-submission 14c application
        /// </summary>
        /// <param name="applicationId">Application Identification Number</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{applicationId}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult DeleteSave(string applicationId)
        {
            AccountController account = new AccountController(_employerService, _organizationService);
            account.UserManager = UserManager;
            var userInfo = account.GetUserInfo();
            // make sure user has rights to the Applicaion
            var hasPermission = _identityService.HasSavePermission(userInfo, applicationId);
            if (!hasPermission)
            {
                Unauthorized("Unauthorized");
            }

            _saveService.Remove(applicationId);
            return Ok();
        }

        /// <summary>
        /// Clear out applciation saved data
        /// </summary>
        /// <param name="applicationId">Application Identification Number</param>
        /// <returns></returns>
        [HttpPost]
        [Route("clearsave")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult ClearApplicationData(string applicationId)
        {
            AccountController account = new AccountController(_employerService, _organizationService);
            account.UserManager = UserManager;
            var userInfo = account.GetUserInfo();
            // make sure user has rights to the Applicaion
            var hasPermission = _identityService.HasSavePermission(userInfo, applicationId);
            if (!hasPermission)
            {
                Unauthorized("Unauthorized");
            }
            var user = UserManager.Users.SingleOrDefault(s => s.Id == userInfo.UserId);
            var organization = user.Organizations.FirstOrDefault(x => x.ApplicationId == applicationId && x.ApplicationStatusId != StatusIds.Submitted);

            // Clear Application Information 
            if (organization != null)
            {
                // Remove application from application save table
                DateTime now = DateTime.UtcNow;
                var state = "{\"saved\":\""+ now.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ") + "\"}";
                _saveService.AddOrUpdate(organization.ApplicationId, organization.ApplicationId, organization.Employer_Id, state);
                // Soft delete application attachements 
                _attachmentService.DeleteApplicationAttachements(applicationId);
            }
            else
            {
                ExpectationFailed(string.Format("Cannot clear this application. Application Id: {0}", applicationId));
            }

            return Ok();
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