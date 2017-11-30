using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using DOL.WHD.Section14c.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>//, IAuditedEntity
    {
        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }

        public ApplicationRole()
        {
            Id = Id ?? Guid.NewGuid().ToString();
        }
        public DateTime LastModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        //public string CreatedBy_Id { get; set; }

        //[ForeignKey("LastModifiedBy_Id")]
        //public ApplicationUser CreatedBy { get; set; }

        //public string LastModifiedBy_Id { get; set; }

        //[ForeignKey("LastModifiedBy_Id")]
        //public ApplicationUser LastModifiedBy { get; set; }
    }
}
