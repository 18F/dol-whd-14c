using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.Domain
{
    public class BaseEntity : IAuditedEntity
    {      
        public string CreatedBy_Id { get; set; }  

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser CreatedBy { get; set; }
        
        private DateTime? createdAt = null;
        public DateTime CreatedAt
        {
            get
            {
                return createdAt ?? DateTime.Now;
            }

            set { this.createdAt = value; }
        }

        public string LastModifiedBy_Id { get; set; }

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser LastModifiedBy { get; set; }

        private DateTime? lastModifiedAt = null;

        public DateTime LastModifiedAt
        {
            get
            {
                return lastModifiedAt ?? DateTime.Now;
            }

            set { this.lastModifiedAt = value; }
        }        
    }
}
