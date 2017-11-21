using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {

        public DateTime LastModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
