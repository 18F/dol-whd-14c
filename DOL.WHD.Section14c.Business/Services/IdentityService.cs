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
        public bool UserHasEINClaim(IPrincipal user, string EIN)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var einClaims = identity.Claims.Where(c => c.Type == "Id").Select(c => c.Value);
            return einClaims.Contains(EIN);
        }

        public bool UserHasFeatureClaim(IPrincipal user, string feature)
        {
            var identity = (ClaimsIdentity)user.Identity;
            return identity.Claims.Any(c => c.Type == feature);
        }

        public bool UserHasAPPIDClaim(IPrincipal user, string ApplicationId)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var einClaims = identity.Claims.Where(c => c.Type == "APPID").Select(c => c.Value);
            return einClaims.Contains(ApplicationId);
        }

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
                var rganization = userInfo.Organizations.SingleOrDefault(x => x.Employer.Id == employerId);
                if (rganization != null)
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
        /// <returns></returns>
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
                var rganization = userInfo.Organizations.SingleOrDefault(x => x.ApplicationId == applicationId);
                if (rganization != null)
                {
                    userHasRight = true;
                }
            }
            return userHasRight;
        }
    }
}
