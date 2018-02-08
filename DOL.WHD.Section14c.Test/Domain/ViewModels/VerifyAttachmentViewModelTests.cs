using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class VerifyAttachmentViewModelTests
    {
        [TestMethod]
        public void VerifyAttachmentViewModel_PublicProperties()
        {
            var obj = new VerifyAttachmentViewModel
            {
                AttachmentId = "AttachmentId",
                AttachmentName = "AttachmentName"
            };

            Assert.AreEqual("AttachmentId", obj.AttachmentId);
            Assert.AreEqual("AttachmentName", obj.AttachmentName);
        }
    }
}
