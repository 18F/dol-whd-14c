using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public sealed class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUserRole()
        {
            RoleId = RoleId ?? Guid.NewGuid().ToString();
        }

        public ApplicationRole Role { get; set; }
    }
}
