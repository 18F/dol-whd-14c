using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class WIOAWorkerTests
    {
        [TestMethod]
        public void WIOAWorker_PublicProperties()
        {
            var fullName = "Full Worker Name";
            var wioaWorkerVerifiedId = 55;
            var wioaWorkerVerified = new Response {Id = wioaWorkerVerifiedId};

            var model = new WIOAWorker
            {
                FullName = fullName,
                WIOAWorkerVerifiedId = wioaWorkerVerifiedId,
                WIOAWorkerVerified = wioaWorkerVerified
            };

            Assert.AreEqual(fullName, model.FullName);
            Assert.AreEqual(wioaWorkerVerifiedId, model.WIOAWorkerVerifiedId);
            Assert.AreEqual(wioaWorkerVerified, model.WIOAWorkerVerified);
        }
    }
}
