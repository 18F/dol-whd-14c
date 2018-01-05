using System;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class AccountBindingModelsTests
    {
        [TestMethod]
        public void ChangePasswordBindingModel_PublicProperties()
        {
            var obj = new ChangePasswordViewModel
            {
                Email = "email",
                ConfirmPassword = "confirmpassword",
                NewPassword = "newpassword",
                OldPassword = "oldpassword"
            };

            Assert.AreEqual("email", obj.Email);
            Assert.AreEqual("confirmpassword", obj.ConfirmPassword);
            Assert.AreEqual("newpassword", obj.NewPassword);
            Assert.AreEqual("oldpassword", obj.OldPassword);
        }

        [TestMethod]
        public void RegisterBindingModel_PublicProperties()
        {
            var uri = new Uri("http://www.gsa.gov");
            var obj = new RegisterViewModel
            {
                Email = "email",
                ConfirmPassword = "confirmpassword",
                Password = "password",
                EmailVerificationUrl = uri,
                FirstName = "John",
                LastName = "Doe"
            };

            Assert.AreEqual("email", obj.Email);
            Assert.AreEqual("confirmpassword", obj.ConfirmPassword);
            Assert.AreEqual("password", obj.Password);
            Assert.AreEqual(uri, obj.EmailVerificationUrl);
            Assert.AreEqual("John", obj.FirstName);
            Assert.AreEqual("Doe", obj.LastName);
        }

        [TestMethod]
        public void RemoveLoginBindingModel_PublicProperties()
        {
            var obj = new RemoveLoginViewModel
            {
                LoginProvider = "loginprovider",
                ProviderKey = "providerkey"
            };

            Assert.AreEqual("loginprovider", obj.LoginProvider);
            Assert.AreEqual("providerkey", obj.ProviderKey);
        }

        [TestMethod]
        public void SetPasswordBindingModel_PublicProperties()
        {
            var obj = new SetPasswordViewModel
            {
                NewPassword = "newpassword",
                ConfirmPassword = "confirmpassword"
            };

            Assert.AreEqual("newpassword", obj.NewPassword);
            Assert.AreEqual("confirmpassword", obj.ConfirmPassword);
        }
    }
}
