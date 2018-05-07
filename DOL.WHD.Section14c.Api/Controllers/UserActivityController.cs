using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DOL.WHD.Section14c.Domain.ViewModels;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Log.LogHelper;
using DOL.WHD.Section14c.DataAccess.Identity;
using Microsoft.AspNet.Identity.Owin;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.Api.Controllers
{

    /// <summary>
    /// Audit controller
    /// </summary>
    //[AuthorizeClaims(ApplicationClaimTypes.AuditAccounts)]
    [AuthorizeHttps]
    [RoutePrefix("api/admin/audit/users")]
    public class UserActivityController : BaseApiController
    {

        private readonly IUserActivityService _userActivityService;
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
            set { _userManager = value; }
        }

        /// <summary>
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="userActivityService">
        public UserActivityController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }



        /// <summary>
        /// Get all the new users
        /// </summary>
        // GET: api/admin/audit/users/created
        //[AuthorizeClaims(ApplicationClaimTypes.AuditAccounts)]
        [AllowAnonymous]
        [HttpGet]
        [Route("created")]
        public IEnumerable<UserActivityViewModel> GetUsersCreated()
        {
            var userActivities = _userActivityService.GetAllAccounts();

            return userActivities.Select(x => new UserActivityViewModel
            {
                Email = x.UserName,
                ActionId = x.ActionId,
                ActionType = x.ActionType
            }).Where(a => a.ActionId == UserActionIds.NewRegistration).ToList();

        }

        /// <summary>
        /// Get all the modified users
        /// </summary>
        // GET: api/admin/audit/users/updated
        [AuthorizeClaims(ApplicationClaimTypes.UserActivities)]
        [Route("updated")]
        public IEnumerable<UserActivityViewModel> GetUsersChanged()
        {
            var getAll = _userActivityService.GetAllAccounts();
            return getAll.Select(x => new UserActivityViewModel
            {
                Email = x.UserName,
                ActionId = x.ActionId,
                ActionType = x.ActionType
            }).Where(a => a.ActionId == UserActionIds.Updated).ToList();

        }

        /// <summary>
        /// Get all the active users
        /// </summary>
        // GET: api/admin/audit/users/active
        [AuthorizeClaims(ApplicationClaimTypes.UserActivities)]
        [Route("active")]
        public IEnumerable<UserActivityViewModel> GetUsersActive()
        {
            var getAll = _userActivityService.GetAllAccounts();
            return getAll.Select(x => new UserActivityViewModel
            {
                Email = x.UserName,
                ActionId = x.ActionId,
                ActionType = x.ActionType
            }).Where(a => a.ActionId == UserActionIds.Enable).ToList();

        }

        /// <summary>
        /// Get all the inactive users
        /// </summary>
        // GET: api/admin/audit/users/inactive
        [AuthorizeClaims(ApplicationClaimTypes.UserActivities)]
        [Route("inactive")]
        public IEnumerable<UserActivityViewModel> GetUsersInactive()
        {
            var getAll = _userActivityService.GetAllAccounts();
            return getAll.Select(x => new UserActivityViewModel
            {
                Email = x.UserName,
                ActionId = x.ActionId,
                ActionType = x.ActionType
            }).Where(a => a.ActionId == UserActionIds.Disable).ToList();

        }

        /// <summary>
        /// Get all the deleted users
        /// </summary>
        // GET: api/admin/audit/users/deleted
        [AuthorizeClaims(ApplicationClaimTypes.UserActivities)]
        [Route("deleted")]
        public IEnumerable<UserActivityViewModel> GetUsersDeleted()
        {
            var getAll = _userActivityService.GetAllAccounts();
            return getAll.Select(x => new UserActivityViewModel
            {
                Email = x.UserName,
                ActionId = x.ActionId,
                ActionType = x.ActionType
            }).Where(a => a.ActionId == UserActionIds.Delete).ToList();

        }


    }
}
