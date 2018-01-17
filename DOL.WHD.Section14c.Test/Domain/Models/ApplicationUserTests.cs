using System;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models
{
    [TestClass]
    public class ApplicationUserTests
    {
        [TestMethod]
        public void ApplicationUser_PublicProperties()
        {
            var testDate = DateTime.Today;
            var firstName = "John";
            var lastName = "Doe";
            var user = new ApplicationUser { Id = "123"};
            var obj = new ApplicationUser
            {
                LastPasswordChangedDate = testDate,
                CreatedAt = testDate,
                LastModifiedAt = testDate,
                FirstName = firstName,
                LastName = lastName,
                CreatedBy = user,
                CreatedBy_Id = "123",
                LastModifiedBy = user,
                LastModifiedBy_Id = "123"
            };

            Assert.AreEqual(testDate, obj.LastPasswordChangedDate);
            Assert.AreEqual(testDate, obj.CreatedAt);
            Assert.AreEqual(testDate, obj.LastModifiedAt);
            Assert.AreEqual(firstName, obj.FirstName);
            Assert.AreEqual(lastName, obj.LastName);
            Assert.AreEqual(user.Id, obj.CreatedBy_Id);
            Assert.AreEqual(user.Id, obj.LastModifiedBy_Id);
        }

        [TestMethod]
        public void ApplicationUser_GenerateUserIdentityAsync()
        {
            var testDate = DateTime.Today;
            var obj = new ApplicationUser
            {
                LastPasswordChangedDate = testDate                           
            };

            Assert.AreEqual(testDate, obj.LastPasswordChangedDate);            
        }
    }
}
