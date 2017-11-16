using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business.Services.Tests
{
    [TestClass()]
    public class EmailServiceTests
    {
        private IEmailService _emailService;
        private string template;

        [TestInitialize]
        public void Initialize()
        {
            _emailService = new EmailService();
            template = @"<email-templates>
                            <certification-team-email-template>
                                <email-to>EmailTo</email-to>
                                <email-subject>Email Subject</email-subject>
                                <email-body>email body</email-body>
                            </certification-team-email-template>
                            <employer-email-template>
                                <email-to>Employer Email to</email-to>
                                <email-subject>Employer Email Subject</email-subject>
                                <email-body>Employer email body</email-body> 
                            </employer-email-template>
                        </email-templates>";
        }

        [TestMethod()]
        public void PrepareApplicationEmailContentsTest()
        {
            ApplicationSubmission applicationSubmission = new ApplicationSubmission();
            var content = _emailService.PrepareApplicationEmailContents(applicationSubmission, template, Helper.EmailReceiver.Both);
            Assert.AreEqual("EmailTo", content["CertificationEmail"].To);
            Assert.AreEqual("Email Subject", content["CertificationEmail"].Subject);
            Assert.AreEqual("email body", content["CertificationEmail"].Body);
            Assert.AreEqual("Employer Email to", content["EmployerEmail"].To);
            Assert.AreEqual("Employer Email Subject", content["EmployerEmail"].Subject);
            Assert.AreEqual("Employer email body", content["EmployerEmail"].Body);
        }
    }
}