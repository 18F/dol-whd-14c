using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOL.WHD.Section14c.Domain
{
    public class BaseEntity : IAuditedEntity
    {
        public string CreatedBy_Id { get; set; }

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastModifiedBy_Id { get; set; }

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser LastModifiedBy { get; set; }

        public DateTime LastModifiedAt { get; set; }
    }
}
