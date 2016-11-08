using System;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class ApplicationSubmissionEstablishmentTypeTests
    {
        [TestMethod]
        public void ApplicationSubmissionEstablishmentType_PublicProperties()
        {
            var applicationSubmission = new ApplicationSubmission() {};
            var response = new Response() {};
            var obj = new ApplicationSubmissionEstablishmentType
            {
                ApplicationSubmission = applicationSubmission,
                ApplicationSubmissionId = Guid.Empty,
                EstablishmentType = response,
                EstablishmentTypeId = 1
            };

            Assert.AreEqual(applicationSubmission, obj.ApplicationSubmission);
            Assert.AreEqual(Guid.Empty, obj.ApplicationSubmissionId);
            Assert.AreEqual(response, obj.EstablishmentType);
            Assert.AreEqual(1, obj.EstablishmentTypeId);
        }
    }
}
