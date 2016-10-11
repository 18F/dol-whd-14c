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
            var obj = new ChangePasswordBindingModel
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
            var obj = new RegisterBindingModel
            {
                Email = "email",
                ConfirmPassword = "confirmpassword",
                Password = "password",
                EIN = "ein",
                ReCaptchaResponse = "recaptchresponse"
            };

            Assert.AreEqual("email", obj.Email);
            Assert.AreEqual("confirmpassword", obj.ConfirmPassword);
            Assert.AreEqual("password", obj.Password);
            Assert.AreEqual("ein", obj.EIN);
            Assert.AreEqual("recaptchresponse", obj.ReCaptchaResponse);
        }

        [TestMethod]
        public void RemoveLoginBindingModel_PublicProperties()
        {
            var obj = new RemoveLoginBindingModel
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
            var obj = new SetPasswordBindingModel
            {
                NewPassword = "newpassword",
                ConfirmPassword = "confirmpassword"
            };

            Assert.AreEqual("newpassword", obj.NewPassword);
            Assert.AreEqual("confirmpassword", obj.ConfirmPassword);
        }
    }
}
