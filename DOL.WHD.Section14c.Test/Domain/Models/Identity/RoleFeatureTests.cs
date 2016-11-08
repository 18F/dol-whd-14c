using System.ComponentModel.DataAnnotations.Schema;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Identity
{
    [TestClass]
    public class RoleFeatureTests
    {
        [TestMethod]
        public void RoleFeature_PublicProperties()
        {
            var applicationRole = new ApplicationRole() {Id = "1"};
            var feature = new Feature() { Id = 1};
            var obj = new RoleFeature
            {

                RoleFeatureId = 1,
                ApplicationRole_Id = "name",
                ApplicationRole = applicationRole,
                Feature_Id = 1,
                Feature = feature

            };

            Assert.AreEqual(1, obj.RoleFeatureId);
            Assert.AreEqual("name", obj.ApplicationRole_Id);
            Assert.AreEqual(applicationRole, obj.ApplicationRole);
            Assert.AreEqual(1, obj.Feature_Id);
            Assert.AreEqual(feature, obj.Feature);
        }
    }
}