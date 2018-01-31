using DOL.WHD.Section14c.Common.Extensions;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DOL.WHD.Section14c.Domain.Models
{
    [DataContract]
    public class OrganizationMembership : IAuditedEntity
    {
        public OrganizationMembership()
        {
            if (string.IsNullOrEmpty(MembershipId))
                MembershipId = Guid.NewGuid().ToString();
        }

        [Key]
        public string MembershipId { get; set; }
        
        [Required]
        [DataMember]
        public string EIN { get; set; }

        [DataMember]
        public string Employer_Id { get; set; }

        [ForeignKey("Employer_Id")]
        [Required]
        [DataMember]
        public virtual Employer Employer { get; set; }

        [DataMember]
        public string ApplicationId { get; set; }

        [Required]
        [DataMember]
        public bool IsPointOfContact { get; set; }

        [DataMember]
        public int? ApplicationStatusId { get; set; }

        [DataMember]
        public virtual Status ApplicationStatus { get; set; }
        [DataMember]
        public string CreatedBy_Id { get; set; }

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser CreatedBy { get; set; }

        private DateTime? createdAt = null;
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public string LastModifiedBy_Id { get; set; }

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser LastModifiedBy { get; set; }

        private DateTime? lastModifiedAt = null;
        [DataMember]
        public DateTime LastModifiedAt { get; set; }
    }
}
