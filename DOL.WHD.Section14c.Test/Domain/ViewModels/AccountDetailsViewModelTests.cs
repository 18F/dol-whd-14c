using System;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class AccountDetailsViewModelTests
    {
        [TestMethod]
        public void VerifyResetPasswordViewModel_PublicProperties()
        {
            var changeDate = DateTime.Now;
            var lockoutDate = DateTime.Now;
            var obj = new AccountDetailsViewModel
            {

                LastPasswordChangedDate = changeDate,
                LockoutEndDateUtc = lockoutDate,
                EmailConfirmed = true
            };

            Assert.AreEqual(changeDate, obj.LastPasswordChangedDate);
            Assert.AreEqual(lockoutDate, obj.LockoutEndDateUtc);
            Assert.AreEqual(true, obj.EmailConfirmed);
        }
    }
}
