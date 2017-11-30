using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using DOL.WHD.Section14c.DataAccess;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public sealed class ApplicationUserRole : IdentityUserRole<string>//, IAuditedEntity
    {
        public ApplicationUserRole()
        {
            RoleId = RoleId ?? Guid.NewGuid().ToString();
        }

        public ApplicationRole Role { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        //public string CreatedBy_Id { get; set; }

        //[ForeignKey("LastModifiedBy_Id")]
        //public ApplicationUser CreatedBy { get; set; }

        //public string LastModifiedBy_Id { get; set; }

        //[ForeignKey("LastModifiedBy_Id")]
        //public  ApplicationUser LastModifiedBy { get; set; }
    }
}
