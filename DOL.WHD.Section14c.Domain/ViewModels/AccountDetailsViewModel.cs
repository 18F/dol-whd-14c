using System;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class AccountDetailsViewModel : UserInfoViewModel
    {
        public DateTime? LastPasswordChangedDate { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
    }
}
