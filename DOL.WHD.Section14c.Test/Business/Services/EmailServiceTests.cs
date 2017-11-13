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
        private readonly IFileRepository _fileRepositoryMock;
        private IEmailService _emailService;
        public EmailServiceTests()
        {
            _fileRepositoryMock = new FileRepository(@"MailTest\");
            SmtpClient client = new SmtpClient();
            _emailService = new EmailService(client);
        }

        [TestMethod()]
        public void SendEmailTest()
        {
            var emailContent = new EmailContent();
            emailContent.To = "test@test.com;test2@test.com";
            emailContent.Subject = "My Test Subject";
            emailContent.Body = "My Test Body";

            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);

            using (var memoryStream = new MemoryStream(data))
            {
                var attachments = new List<Attachment>();
                attachments.Add(new Attachment(memoryStream, "test.txt"));
                emailContent.attachments = attachments;
            }

            var result = _emailService.SendEmail(emailContent);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ApiException))]
        public void SendEmailTest_NoRecipients_Invalid()
        {
            var emailContent = new EmailContent();
            emailContent.To = "";
            emailContent.Subject = "My Test Subject";
            emailContent.Body = "My Test Body";

            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);

            using (var memoryStream = new MemoryStream(data))
            {
                var attachments = new List<Attachment>();
                attachments.Add(new Attachment(memoryStream, "test.txt"));
                emailContent.attachments = attachments;
            }

            var result = _emailService.SendEmail(emailContent);
        }
    }
}