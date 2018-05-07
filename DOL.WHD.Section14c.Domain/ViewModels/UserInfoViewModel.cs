using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    // Models returned by AccountController actions.
    public class UserInfoViewModel
    {
        public UserInfoViewModel()
        {
            if (Roles == null) Roles = new List<RoleViewModel>();
            if (ApplicationClaims == null) ApplicationClaims = new List<string>();
        }
        public string UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public IEnumerable<OrganizationMembership> Organizations { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public IEnumerable<string> ApplicationClaims { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
        public bool TwoFactorEnabled { get; set; }

    }
}
