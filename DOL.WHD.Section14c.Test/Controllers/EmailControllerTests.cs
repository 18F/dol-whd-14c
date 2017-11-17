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
        private Mock<IEmailService> _emailServiceMock;
        [TestInitialize]
        public void Initialize()
        {
            _emailContent = new EmailContent() {
                To = "test@test.com;test2@test.com",
                Subject = "My Test Subject",
                Body = "My Test Body"
            };

            _emailServiceMock = new Mock<IEmailService>();
            _emailController = new EmailController(_emailServiceMock.Object);
        }

        [TestMethod()]
        public void SendEmailTest()
        {            
            // Setup
            _emailServiceMock.Setup(mock => mock.SendEmail(It.IsAny<EmailContent>())).Returns(true);
      
            // Execute
            IHttpActionResult result = _emailController.SendEmail(_emailContent);
            var contentResult = result as OkNegotiatedContentResult<Boolean>;

            // Assert
            Assert.IsTrue(contentResult.Content);
            _emailServiceMock.Verify(mock => mock.SendEmail(It.IsAny<EmailContent>()), Times.Once());
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