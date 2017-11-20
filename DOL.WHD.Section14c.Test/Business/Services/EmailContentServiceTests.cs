using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Moq;

namespace DOL.WHD.Section14c.Business.Services.Tests
{
    [TestClass()]
    public class EmailContentServiceTests
    {
        private IEmailContentService _emailService;
        private IApplicationService _applicationServiceMock;
        private ApplicationSubmission application;
        private string certificationTeamEmailBodyTemplate;
        private string employerEmailBodyTemplate;

        [TestInitialize]
        public void Initialize()
        {
            _applicationServiceMock = new ApplicationServiceMock();
            application = _applicationServiceMock.GetApplicationById(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F666"));
            _emailService = new EmailContentService();
            certificationTeamEmailBodyTemplate = @"email body";
            employerEmailBodyTemplate = @"Employer email body";
        }

        [TestMethod()]
        public void PrepareApplicationEmailContentsTest()
        {
            var content = _emailService.PrepareApplicationEmailContents(application, certificationTeamEmailBodyTemplate, employerEmailBodyTemplate, Helper.EmailReceiver.Both);
            Assert.AreEqual("test@test.com", content["CertificationEmail"].To);
            Assert.AreEqual("VA :: Email Subject", content["CertificationEmail"].Subject);
            Assert.AreEqual("email body", content["CertificationEmail"].Body);
            Assert.AreEqual("test@test.com", content["EmployerEmail"].To);
            Assert.AreEqual("Email Subject", content["EmployerEmail"].Subject);
            Assert.AreEqual("Employer email body", content["EmployerEmail"].Body);
        }
    }
}