using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.EmailApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.EmailApi.Business;
using System.Net.Mail;
using System.IO;
using DOL.WHD.Section14c.EmailApi.Helper;
using System.Web.Http.Results;
using System.Net;
using System.Web.Http;
using Moq;

namespace DOL.WHD.Section14c.EmailApi.Controllers.Tests
{
    [TestClass()]
    public class EmailControllerTests
    {

        //private IEmailService _emailService;
        private EmailController _emailController;
        private EmailContent _emailContent;

        [TestInitialize]
        public void Initialize()
        {
            //string testEmailPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestEmails"));
            //if (!System.IO.Directory.Exists(testEmailPath))
            //    System.IO.Directory.CreateDirectory(testEmailPath);

            //SmtpClient client = new SmtpClient()
            //{
            //    PickupDirectoryLocation = testEmailPath
            //};

            //_emailService = new EmailService(client);

            _emailContent = new EmailContent();
            _emailContent.To = "test@test.com;test2@test.com";
            _emailContent.Subject = "My Test Subject";
            _emailContent.Body = "My Test Body";

            //var testFileContents = "test";
            //var data = Encoding.ASCII.GetBytes(testFileContents);
            //Dictionary<string, byte[]> attachments = new Dictionary<string, byte[]>();
            //attachments.Add("test.txt", data);
            //_emailContent.Attachments = attachments;


            var _emailServiceMock = new Mock<IEmailService>();
            _emailController = new EmailController(_emailServiceMock.Object);
        }

        [TestMethod()]
        public void SendEmailTest()
        {
            IHttpActionResult result = _emailController.SendEmail(_emailContent);
            var contentResult = result as OkNegotiatedContentResult<Boolean>;
            Assert.IsFalse(contentResult.Content);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendEmail_NullException_Test()
        {
            var result = _emailController.SendEmail(null);
        }

        [TestMethod]
        public void SendEmail_CORS()
        {
            var response = _emailController.Options();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}