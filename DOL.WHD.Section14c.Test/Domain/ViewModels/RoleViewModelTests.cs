using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class RoleViewModelTests
    {
        [TestMethod]
        public void RoleViewModel_PublicProperties()
        {
            var obj = new RoleViewModel
            {
                Id = "id",
                Name = "name"
            };

            Assert.AreEqual("id", obj.Id);
            Assert.AreEqual("name", obj.Name);
        }
    }
}
