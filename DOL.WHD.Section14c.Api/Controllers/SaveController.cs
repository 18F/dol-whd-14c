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
        /// <param name="saveService"></param>
        /// <param name="identityService"></param>
        /// <param name="organizationService"></param>
        /// <param name="employerService"></param>
        public SaveController(ISaveService saveService, IIdentityService identityService, IOrganizationService organizationService, IEmployerService employerService)
        {
            _saveService = saveService;
            _identityService = identityService;
            _organizationService = organizationService;
            _employerService = employerService;
        }

        /// <summary>
        /// Returns pre-submission 14c application
        /// </summary>
        /// <param name="applicationId">Employer Identification Number</param>
        [HttpGet]
        [Route("{EIN}")]
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
        /// <param name="EIN"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{EIN}/{employerId}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult AddSave(string EIN, string employerId)
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

            var applicationId = Guid.NewGuid().ToString();
            _saveService.AddOrUpdate(EIN, applicationId, state);

            // Set Application Id for organization 
            var organization = userInfo.Organizations.SingleOrDefault(x => x.Employer.Id == employerId && string.IsNullOrEmpty( x.ApplicationId));
            if (organization != null)
            {
                organization.ApplicationId = applicationId;
                _organizationService.UpdateOrganizationMembership(organization);
            }

            // Update Organization Status
            var user = UserManager.Users.SingleOrDefault(s => s.Id == userInfo.UserId);
            user.Organizations.FirstOrDefault(x=>x.ApplicationId == applicationId).ApplicationStatusId = StatusIds.InProgress;
            UserManager.UpdateAsync(user);

            return Created($"/api/Save?userId={User.Identity.GetUserId()}&EIN={EIN}", new { });
        }

        /// <summary>
        /// Creates or updates pre-submission 14c application
        /// </summary>
        /// <param name="EIN">EIN</param>
        /// /// <param name="applicationId">Application Id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateSave")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult UpdateSave(string applicationId, string EIN)
        {
            AccountController account = new AccountController( _employerService, _organizationService);
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

            _saveService.AddOrUpdate(EIN, applicationId, state);
            return Created($"/api/Save?userId={User.Identity.GetUserId()}&EIN={EIN}", new { });
        }

        /// <summary>
        /// Removes pre-submission 14c application
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{EIN}")]
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
        /// OPTIONS endpoint for CORS
        /// </summary>
        [AllowAnonymous]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}