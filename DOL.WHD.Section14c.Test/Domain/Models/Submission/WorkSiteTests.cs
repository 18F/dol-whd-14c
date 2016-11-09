using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class WorkSiteTests
    {
        [TestMethod]
        public void WorkSite_PublicProperties()
        {
            var workSiteTypeId = 36;
            var workSiteType = new Response {Id = workSiteTypeId};
            var name = "Worksite Name";
            var address = new Address();
            var sca = true;
            var federalContractWorkPerformed = true;
            var numEmployees = 5;
            var employees = new List<Employee>();

            var model = new WorkSite
            {
                WorkSiteTypeId = workSiteTypeId,
                WorkSiteType = workSiteType,
                Name = name,
                Address = address,
                SCA = sca,
                FederalContractWorkPerformed = federalContractWorkPerformed,
                NumEmployees = numEmployees,
                Employees = employees
            };

            Assert.AreEqual(workSiteTypeId, model.WorkSiteTypeId);
            Assert.AreEqual(workSiteType, model.WorkSiteType);
            Assert.AreEqual(name, model.Name);
            Assert.AreEqual(address, model.Address);
            Assert.AreEqual(sca, model.SCA);
            Assert.AreEqual(federalContractWorkPerformed, model.FederalContractWorkPerformed);
            Assert.AreEqual(numEmployees, model.NumEmployees);
            Assert.AreEqual(employees, model.Employees);
        }
    }
}
