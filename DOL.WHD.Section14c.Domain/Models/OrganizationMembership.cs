using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DOL.WHD.Section14c.Domain.Models
{
    [DataContract]
    public class OrganizationMembership
    {
        public OrganizationMembership()
        {
            MembershipId = Guid.NewGuid();
        }

        [Key]
        public Guid MembershipId { get; set; }

        [Required]
        [DataMember]
        public string EIN { get; set; }

        [Required]
        [DataMember]
        public bool IsAdmin { get; set; }
    }
}
