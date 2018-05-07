using System;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class AccountDetailsViewModel : UserInfoViewModel //TODO Partition columns across the two classes(UserInfoViewModel, AccountDetailsViewModel) based on the security considerations e.g PhoneConfirmed
    {
        public DateTime? LastPasswordChangedDate { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
