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
            var obj = new ApplicationUser
            {
                LastPasswordChangedDate = testDate
            };

            Assert.AreEqual(testDate, obj.LastPasswordChangedDate);
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
