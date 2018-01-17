using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DOL.WHD.Section14c.Test.Domain.Models.Identity
{
    [TestClass]
    public class ApplicationUserRoleTests
    {
        [TestMethod]
        public void ApplicationUserRole_PublicProperties()
        {
            var applicationRole = new ApplicationRole() {Id = "id"};
            var testDate = DateTime.Today;
            var user = new ApplicationUser { Id = "123" };

            var obj = new ApplicationUserRole
            {
                Role = applicationRole,
                CreatedAt = testDate,
                LastModifiedAt = testDate,
                CreatedBy = user,
                CreatedBy_Id = "123",
                LastModifiedBy = user,
                LastModifiedBy_Id = "123"
            };

            Assert.AreEqual(applicationRole, obj.Role);
            Assert.AreEqual(testDate, obj.CreatedAt);
            Assert.AreEqual(testDate, obj.LastModifiedAt);
            Assert.AreEqual(user.Id, obj.CreatedBy_Id);
            Assert.AreEqual(user.Id, obj.LastModifiedBy_Id);
        }

        [TestMethod]
        public void ApplicationUserRole_ConstructorSetRoleID()
        {
            var obj = new ApplicationUserRole();
            Assert.IsNotNull(obj.RoleId);
        }
    }
}