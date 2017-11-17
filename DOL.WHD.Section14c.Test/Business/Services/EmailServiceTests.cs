using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.EmailApi.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.EmailApi.Helper;
using System.IO;
using System.Net.Mail;
using DOL.WHD.Section14c.Log.LogHelper;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;

namespace DOL.WHD.Section14c.EmailApi.Business.Tests
{
    [TestClass()]
    public class EmailServiceTests
    {
        private IEmailService _emailService;
        public EmailServiceTests()
        {
            string testEmailPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestEmails"));
            if (!System.IO.Directory.Exists(testEmailPath))
                System.IO.Directory.CreateDirectory(testEmailPath);

            SmtpClient client = new SmtpClient()
            {
                PickupDirectoryLocation = testEmailPath
            };

            _emailService = new EmailService(client);
        }

        [TestMethod()]
        public void SendEmail_Test()
        {
            var emailContent = new EmailContent();
            emailContent.To = "test@test.com";
            emailContent.CC = "test1@test.com;test2@test.com";
            emailContent.Subject = "My Test Subject";
            emailContent.Body = "My Test Body";

            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            Dictionary<string, byte[]> attachments = new Dictionary<string, byte[]>() {
                {"test.txt", data }
            };

            emailContent.Attachments = attachments;

            var result = _emailService.SendEmail(emailContent);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ApiException))]
        public void SendEmail_NoRecipients_Test()
        {
            var emailContent = new EmailContent();
            emailContent.To = "";
            emailContent.Subject = "My Test Subject";
            emailContent.Body = "My Test Body";

            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            Dictionary<string, byte[]> attachments = new Dictionary<string, byte[]>() {
                {"test.txt", data }
            };

            emailContent.Attachments = attachments;

            var result = _emailService.SendEmail(emailContent);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendEmail_NullException_Test()
        {
            var result = _emailService.SendEmail(null);
        }
    }
}