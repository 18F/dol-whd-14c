using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class OrganizationMembership
    {
        public OrganizationMembership()
        {
            MembershipId = Guid.NewGuid();
        }

        [Key]
        public Guid MembershipId { get; set; }

        [Required]
        public string EIN { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}
