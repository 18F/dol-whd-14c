using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class VerifyResetPasswordViewModelTests
    {
        [TestMethod]
        public void VerifyResetPasswordViewModel_PublicProperties()
        {
            var obj = new VerifyResetPasswordViewModel
            {
                UserId = "userid",
                NewPassword = "newpassword",
                ConfirmPassword = "confirmpassword",
                Nounce = "nounce"
            };

            Assert.AreEqual("userid", obj.UserId);
            Assert.AreEqual("newpassword", obj.NewPassword);
            Assert.AreEqual("confirmpassword", obj.ConfirmPassword);
            Assert.AreEqual("nounce", obj.Nounce);
        }
    }
}
