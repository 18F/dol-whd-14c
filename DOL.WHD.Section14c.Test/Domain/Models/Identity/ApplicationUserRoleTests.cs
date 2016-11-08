using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Identity
{
    [TestClass]
    public class ApplicationUserRoleTests
    {
        [TestMethod]
        public void ApplicationUserRole_PublicProperties()
        {
            var applicationRole = new ApplicationRole() {Id = "id"};
            var obj = new ApplicationUserRole
            {
                Role = applicationRole
            };

            Assert.AreEqual(applicationRole, obj.Role);
        }

        [TestMethod]
        public void ApplicationUserRole_ConstructorSetRoleID()
        {
            var obj = new ApplicationUserRole();
            Assert.IsNotNull(obj.RoleId);
        }
    }
}