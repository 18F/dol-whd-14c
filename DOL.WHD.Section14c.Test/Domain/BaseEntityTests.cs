using System;
using DOL.WHD.Section14c.Domain;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain
{
    [TestClass]
    public class BaseEntityTests
    {
        [TestMethod]
        public void BaseEntity_PublicProperties()
        {
            var testDate = DateTime.Today;
            var testuser = new ApplicationUser() {Id = "1"};
            var obj = new BaseEntity
            {
                CreatedAt = testDate,
                CreatedBy = testuser,
                CreatedBy_Id = "1",
                LastModifiedAt = testDate,
                LastModifiedBy = testuser,
                LastModifiedBy_Id = "1"
            };

            Assert.AreEqual(testDate, obj.CreatedAt);
            Assert.AreEqual(testuser, obj.CreatedBy);
            Assert.AreEqual("1", obj.CreatedBy_Id);
            Assert.AreEqual(testDate, obj.LastModifiedAt);
            Assert.AreEqual(testuser, obj.LastModifiedBy);
            Assert.AreEqual("1", obj.LastModifiedBy_Id);
        }
    }
}
