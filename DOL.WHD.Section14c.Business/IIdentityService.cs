using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.Domain.ViewModels;
using System.Security.Principal;

namespace DOL.WHD.Section14c.Business
{
    public interface IIdentityService
    {
        bool UserHasEINClaim(IPrincipal user, string EIN);

        bool UserHasFeatureClaim(IPrincipal user, string feature);

        bool HasAddPermission(UserInfoViewModel userInfo, string employerId);

        bool HasSavePermission(UserInfoViewModel userInfo, string applicationId);
    }
}
