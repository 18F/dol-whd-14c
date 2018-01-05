using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Business.Helper;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Test.Helpers
{
    [TestClass]
    public class UserApplicationsTests
    {
        [TestMethod]
        public void UserApplications_PublicProperties()
        {
            var employerName ="My Employer";
            Dictionary<string, string> id = new Dictionary<string, string>() {
                {"key1", "value1"},
                {"key2", "value2"},
                {"key3", "value3"}
            };
            var obj = new UserApplications
            {
                EmployerName = employerName,
                Id = id
            };

            Assert.AreEqual(employerName, obj.EmployerName);
            Assert.AreEqual(id, obj.Id);
        }
    }
}
