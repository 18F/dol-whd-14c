using System;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [DataMember]
        public bool IsAdmin { get; set; }
    }
}
