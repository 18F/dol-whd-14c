using DOL.WHD.Section14c.DataAccess;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<string>, IAuditedEntity
    {

        public DateTime LastModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy_Id { get; set; }

        [ForeignKey("CreatedBy_Id")]
        public ApplicationUser CreatedBy { get; set; }

        public string LastModifiedBy_Id { get; set; }

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser LastModifiedBy { get; set; }
    }
}
