using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DOL.WHD.Section14c.Domain.Models
{
    [DataContract]
    public class OrganizationMembership : BaseEntity
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
        public bool IsAdmin { get; set; }
    }
}
