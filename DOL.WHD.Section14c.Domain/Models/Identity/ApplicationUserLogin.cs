using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public DateTime LastModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
