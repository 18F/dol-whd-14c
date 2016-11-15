using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class StatusTests
    {
        [TestMethod]
        public void Status_PublicProperties()
        {
            var id = 5;
            var name = "Status Name";
            var isActive = true;

            var obj = new Status
            {
                Id = id,
                Name = name,
                IsActive = isActive
            };

            Assert.AreEqual(id, obj.Id);
            Assert.AreEqual(name, obj.Name);
            Assert.AreEqual(isActive, obj.IsActive);
        }
    }
}
