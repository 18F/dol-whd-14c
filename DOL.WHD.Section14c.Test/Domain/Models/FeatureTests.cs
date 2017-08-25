using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models
{
    [TestClass]
    public class FeatureTests
    {

        [TestMethod]
        public void Feature_PublicProperties()
        {
            var roleFeatues = new List<RoleFeature>() {new RoleFeature() {}};
            var obj = new Feature
            {
                Id = 1,
                Description = "desc",
                Key = "key",
                RoleFeatures = roleFeatues
            };

            Assert.AreEqual(1, obj.Id);
            Assert.AreEqual("desc", obj.Description);
            Assert.AreEqual("key", obj.Key);
            Assert.AreEqual(roleFeatues, obj.RoleFeatures);
        }
    }
}
