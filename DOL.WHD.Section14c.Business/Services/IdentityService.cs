using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.DataAccess.Identity;
using Microsoft.AspNet.Identity;
using DOL.WHD.Section14c.Domain.ViewModels;

namespace DOL.WHD.Section14c.Business.Services
{
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// Check if user has EIN Claim
        /// </summary>
        /// <param name="user">Principal</param>
        /// <param name="EIN">EIN</param>
        /// <returns>Boolean</returns>
        public bool UserHasEINClaim(IPrincipal user, string EIN)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var einClaims = identity.Claims.Where(c => c.Type == "Id").Select(c => c.Value);
            return einClaims.Contains(EIN);
        }

        /// <summary>
        /// Check if user has Claim
        /// </summary>
        /// <param name="user">Principal</param>
        /// <param name="feature">Claims Feature</param>
        /// <returns>Boolean</returns>
        public bool UserHasFeatureClaim(IPrincipal user, string feature)
        {
            var identity = (ClaimsIdentity)user.Identity;
            return identity.Claims.Any(c => c.Type == feature);
        }

        /// <summary>
        /// Check User Create New Application Permission
        /// </summary>
        /// <param name="userInfo">Application User</param>
        /// <param name="employerId">Employer GUID</param>
        /// <returns>Boolean</returns>
        public bool HasAddPermission(UserInfoViewModel userInfo, string employerId)
        {
            bool userHasRight = false;
            var systemAdminRole = userInfo.Roles.SingleOrDefault(x => x.Name == Roles.SystemAdministrator);
            if (systemAdminRole != null)
            {
                return true;
            }
            else
            {
                var organization = userInfo.Organizations.FirstOrDefault(x => x.Employer.Id == employerId);
                if (organization != null)
                {
                    userHasRight = true;
                }
            }
            return userHasRight;
        }

        /// <summary>
        /// User can save application
        /// </summary>
        /// <param name="user">Application User</param>
        /// <param name="applicationId">Application Id</param>
        /// <returns>Boolean</returns>
        public bool HasSavePermission(UserInfoViewModel userInfo, string applicationId)
        {
            bool userHasRight = false;
            var systemAdminRole = userInfo.Roles.SingleOrDefault(x => x.Name == Roles.SystemAdministrator);
            if (systemAdminRole != null)
            {
                return true;
            }
            else
            {
                var organization = userInfo.Organizations.FirstOrDefault(x => x.ApplicationId == applicationId);
                if (organization != null)
                {
                    userHasRight = true;
                }
            }
            return userHasRight;
        }

        /// <summary>
        /// User is a system admin
        /// </summary>
        /// <param name="user">Application User</param>
        /// <returns>Boolean</returns>
        public bool HasSystemAdminRole(UserInfoViewModel userInfo)
        {
            bool isSystemAdim = false;
            var systemAdminRole = userInfo.Roles.SingleOrDefault(x => x.Name == Roles.SystemAdministrator);
            if (systemAdminRole != null)
            {
                return true;
            }           
            return isSystemAdim;
        }
    }
}
