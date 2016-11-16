using System;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.Business.Factories;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Factories
{
    [TestClass]
    public class ApplicationSummaryFactoryTests
    {
        private readonly IApplicationSummaryFactory _factory;
        public ApplicationSummaryFactoryTests()
        {
            _factory = new ApplicationSummaryFactory();   
        }

        [TestMethod]
        public void ApplicationSummaryFactory_Build()
        {
            // Arrange
            var appId = Guid.NewGuid();
            var applicationType = "New";
            var certificateStatusName = "Issued";
            var certificateEffectiveDate = DateTime.Now;
            var certificateExpirationDate = DateTime.Now;
            var certificateNumber = "xxxxxx";
            var state = "OH";
            var types = new List<string> {"Business", "Hospital"};
            var numWorkSites = 3;
            var numWorkersPerSite = 5;
            var employerName = "Employer Name";
            var submission = new ApplicationSubmission
            {
                Id = appId,
                ApplicationType = new Response {Display = applicationType},
                Status = new Status {Name = certificateStatusName},
                CertificateEffectiveDate = certificateEffectiveDate,
                CertificateExpirationDate = certificateExpirationDate,
                CertificateNumber = certificateNumber,
                EstablishmentType =
                    types.Select(
                            x => new ApplicationSubmissionEstablishmentType {EstablishmentType = new Response {Display = x}})
                        .ToList(),
                Employer = new EmployerInfo
                {
                    PhysicalAddress = new Address {State = state},
                    LegalName = employerName
                },
                WorkSites =
                    Enumerable.Repeat(
                        new WorkSite {Employees = Enumerable.Repeat(new Employee(), numWorkersPerSite).ToList()},
                        numWorkSites).ToList()
            };

            // Act
            var summary = _factory.Build(submission);

            // Assert
            Assert.AreEqual(appId, summary.Id);
            Assert.AreEqual(certificateStatusName, summary.StatusName);
            Assert.AreEqual(certificateEffectiveDate, summary.CertificateEffectiveDate);
            Assert.AreEqual(certificateExpirationDate, summary.CertificateExpirationDate);
            Assert.AreEqual(certificateNumber, summary.CertificateNumber);
            for (int i = 0; i < types.Count; i++)
            {
                Assert.AreEqual(types[i], summary.CertificateType.ElementAt(i));
            }
            Assert.AreEqual(state, summary.State);
            Assert.AreEqual(numWorkSites, summary.NumWorkSites);
            Assert.AreEqual(numWorkSites * numWorkersPerSite, summary.NumWorkers);
            Assert.AreEqual(applicationType, summary.ApplicationType);
            Assert.AreEqual(employerName, summary.EmployerName);
        }
    }
}
