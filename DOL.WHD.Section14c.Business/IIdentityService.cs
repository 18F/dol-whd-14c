using System.Security.Principal;

namespace DOL.WHD.Section14c.Business
{
    public interface IIdentityService
    {
        bool UserHasEINClaim(IPrincipal user, string EIN);

    }
}
