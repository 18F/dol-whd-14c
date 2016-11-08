using System;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class ResetPasswordViewModelTests
    {
        [TestMethod]
        public void ResetPasswordViewModel_PublicProperties()
        {
            var url = new Uri("http://test");
            var obj = new ResetPasswordViewModel
            {
                Email = "email",
                PasswordResetUrl = url
            };

            Assert.AreEqual("email", obj.Email);
            Assert.AreEqual(url, obj.PasswordResetUrl);
        }
    }
}
