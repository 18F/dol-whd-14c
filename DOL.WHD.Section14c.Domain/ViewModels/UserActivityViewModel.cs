using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class UserActivityViewModel
    {
        public UserActivityViewModel()
        {
            if (Roles == null) Roles = new List<RoleViewModel>();
        }
        public string UserId { get; set; }
        public string Email { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public IEnumerable<string> ApplicationClaims { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public int ActionId { get; set; }
        public string ActionType { get; set; }
        public UserInfoViewModel UserInformation { get; set; }
    }
}
