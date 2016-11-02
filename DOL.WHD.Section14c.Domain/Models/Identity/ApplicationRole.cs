using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }

        public ApplicationRole()
        {
            Id = Id ?? Guid.NewGuid().ToString();
        }
    }


}
