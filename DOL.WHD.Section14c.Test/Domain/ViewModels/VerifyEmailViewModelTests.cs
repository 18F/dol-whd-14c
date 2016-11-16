using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class VerifyEmailViewModelTests
    {
        [TestMethod]
        public void VerifyEmailViewModel_PublicProperties()
        {
            var obj = new VerifyEmailViewModel
            {
                UserId = "userid",
                Nounce = "nounce"
            };

            Assert.AreEqual("userid", obj.UserId);
            Assert.AreEqual("nounce", obj.Nounce);
        }
    }
}
