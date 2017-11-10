using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Business;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ApplicationFormTest
    {
        private readonly IFileRepository _fileRepositoryMock;
        private readonly IAttachmentRepository _attachmentRepositoryMock;
        private readonly IApplicationService _applicationServiceMock;
        private ApplicationSubmission application;
        
        public ApplicationFormTest()
        {
            _fileRepositoryMock = new FileRepository(@"TestUploads\");
            _attachmentRepositoryMock = new AttachmentRepositoryMock();
            _applicationServiceMock = new ApplicationServiceMock();
        }

        [TestInitialize]
        public void Initialize()
        {
            application = _applicationServiceMock.GetApplicationById(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F666"));            
        }

        [TestMethod]
        public void PrepareApplicationContentsForPdfConcatenationTest()
        {
            // Arrange
            var einToTest = "40-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            using (var memoryStream = new MemoryStream(data))
            {
                var fileName = "test.txt";

                // Arrange
                var einToTest1 = "40-9876544";
                var fileName1 = "test1.txt";

                var htmlContent = "<html><body><p>Test Test</p></body></html>";
                var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
                var attachment = service.UploadAttachment(einToTest, memoryStream.ToArray(), fileName, "text/plain");
                var attachment1 = service.UploadAttachment(einToTest1, memoryStream.ToArray(), fileName1, "text/plain");

                List<Attachment> attachments = new List<Attachment>()
                {
                    attachment,
                    attachment1
                };

                using (var outMemoryStream = new MemoryStream())
                {
                    List<PDFContentData> applicationDataCollection = service.PrepareApplicationContentsForPdfConcatenation(attachments, htmlContent);

                    string outText = Encoding.ASCII.GetString(applicationDataCollection[1].Buffer);

                    Assert.AreEqual(outText, testFileContents);
                }
            }
        }

        [TestMethod]
        public void GetAllApplicationAttachmentsTest()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            using (var outMemoryStream = new MemoryStream())
            {
                List<Attachment> attachmentArray = service.GetApplicationAttachments(application);
                Assert.AreEqual(5, attachmentArray.Count);
            }
        }

        [TestMethod]
        public void ApplicationFormViewTest()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            string applicationFormHtmlContent = service.GetApplicationFormViewContent(application, DOL.WHD.Section14c.Test.Helpers.Constants.htmlContent);
            Assert.IsNotNull(applicationFormHtmlContent);
        }
    }
}
