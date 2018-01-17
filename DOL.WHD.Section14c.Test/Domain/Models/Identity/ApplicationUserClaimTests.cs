using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.Test.Domain.Models.Identity
{
    [TestClass]
    public class ApplicationUserClaimTests
    {
        [TestMethod]
        public void ApplicationUserClaim_PublicProperties()
        {
            var testDate = DateTime.Today;
            var user = new ApplicationUser { Id = "123" };

            var obj = new ApplicationUserClaim
            {
                CreatedAt = testDate,
                LastModifiedAt = testDate,
                CreatedBy = user,
                CreatedBy_Id = "123",
                LastModifiedBy = user,
                LastModifiedBy_Id = "123"
            };
            
            Assert.AreEqual(testDate, obj.CreatedAt);
            Assert.AreEqual(testDate, obj.LastModifiedAt);
            Assert.AreEqual(user.Id, obj.CreatedBy_Id);
            Assert.AreEqual(user.Id, obj.LastModifiedBy_Id);
        }
    }
}
