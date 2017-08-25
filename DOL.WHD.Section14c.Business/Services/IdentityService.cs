using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.Business.Services
{
    public class IdentityService : IIdentityService
    {
        public bool UserHasEINClaim(IPrincipal user, string EIN)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var einClaims = identity.Claims.Where(c => c.Type == "EIN").Select(c => c.Value);
            return einClaims.Contains(EIN);
        }

        public bool UserHasFeatureClaim(IPrincipal user, string feature)
        {
            var identity = (ClaimsIdentity)user.Identity;
            return identity.Claims.Any(c => c.Type == feature);
        }
    }
}
