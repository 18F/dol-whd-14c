using System;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class EmployerInfoProvidingFacilitiesDeductionTypeTests
    {
        [TestMethod]
        public void EmployerInfoProvidingFacilitiesDeductionType_PublicProperties()
        {
            var employerInfoId = Guid.NewGuid().ToString();
            var employerInfo = new EmployerInfo {Id = employerInfoId };
            var providingFacilitiesDeductionTypeId = 30;
            var providingFacilitiesDeductionType = new Response {Id = providingFacilitiesDeductionTypeId};

            var model = new EmployerInfoProvidingFacilitiesDeductionType
            {
                EmployerInfoId = employerInfoId,
                EmployerInfo = employerInfo,
                ProvidingFacilitiesDeductionTypeId = providingFacilitiesDeductionTypeId,
                ProvidingFacilitiesDeductionType = providingFacilitiesDeductionType
            };

            Assert.AreEqual(employerInfoId, model.EmployerInfoId);
            Assert.AreEqual(employerInfo, model.EmployerInfo);
            Assert.AreEqual(providingFacilitiesDeductionTypeId, model.ProvidingFacilitiesDeductionTypeId);
            Assert.AreEqual(providingFacilitiesDeductionType, model.ProvidingFacilitiesDeductionType);
        }
    }
}
