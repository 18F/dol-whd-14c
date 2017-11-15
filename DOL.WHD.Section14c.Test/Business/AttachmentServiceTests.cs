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
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System.Collections.Generic;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System.Reflection;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class AttachmentServiceTests
    {
        private readonly IFileRepository _fileRepositoryMock;
        private readonly IAttachmentRepository _attachmentRepositoryMock;
        private readonly IApplicationService _applicationServiceMock;
        private ApplicationSubmission application;

        public AttachmentServiceTests()
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
        public void AttachmentsSave()
        {
            // Arrange
            var einToTest = "30-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.txt";

            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            var upload = service.UploadAttachment(einToTest, data, fileName, "text/plain");

            using (var outMemoryStream = new MemoryStream())
            {
                service.DownloadAttachment(outMemoryStream, einToTest, new Guid( upload.Id));

                string outText = Encoding.ASCII.GetString(outMemoryStream.ToArray());

                Assert.AreEqual(outText, testFileContents);
            }
        }

        [TestMethod]
        public void AttachmentsSaveExisting()
        {
            // Arrange
            var einToTest = "30-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.txt";

            // Arrange
            var newData = new ApplicationSave
            {
                EIN = einToTest,
                ApplicationState = "{ \"name\": \"Joe Biden\", \"email:\" \"vice.president@whitehouse.gov\" }"
            };

            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);

            // Act
            var upload = service.UploadAttachment(einToTest, data, fileName, "text/plain");

            using (var outMemoryStream = new MemoryStream())
            {
                service.DownloadAttachment(outMemoryStream, einToTest, new Guid( upload.Id));

                string outText = Encoding.ASCII.GetString(outMemoryStream.ToArray());

                Assert.AreEqual(outText, testFileContents);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void AttachmentNotFound()
        {
            // Arrange
            var einToTest = "30-9876543";

            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            using (var outMemoryStream = new MemoryStream())
            {
                service.DownloadAttachment(outMemoryStream, einToTest, Guid.Empty);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileNotFoundException()
        {
            // Arrange
            var einToTest = "99-9999999";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.txt";

            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            var upload = service.UploadAttachment(einToTest, data, fileName, "text/plain");

            var existingObj = _attachmentRepositoryMock.Get().FirstOrDefault(x => x.EIN == einToTest);
            existingObj.RepositoryFilePath = "invalidPath";
            _attachmentRepositoryMock.SaveChanges();

            using (var outMemoryStream = new MemoryStream())
            {
                service.DownloadAttachment(outMemoryStream, einToTest, new Guid( upload.Id));

                string outText = Encoding.ASCII.GetString(outMemoryStream.ToArray());

                Assert.AreEqual(outText, testFileContents);
            }
        }

        [TestMethod]
        public void DeleteAttachement()
        {
            // Arrange
            var einToTest = "30-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.txt";

            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            var upload = service.UploadAttachment(einToTest, data, fileName, "text/plain");

            service.DeleteAttachement(einToTest, new Guid( upload.Id));

            Assert.IsTrue(upload.Deleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void DeleteNotFound()
        {
            // Arrange
            var einToTest = "30-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.txt";

            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            service.UploadAttachment(einToTest, data, fileName, "text/plain");

            service.DeleteAttachement(einToTest, Guid.Empty);
        }        

        [TestMethod]
        public void Dispose()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            service.Dispose();
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

                List<PDFContentData> applicationDataCollection = service.PrepareApplicationContentsForPdfConcatenation(attachments, htmlContent);

                Assert.AreEqual(3, applicationDataCollection.Count);
                string outText = Encoding.ASCII.GetString(applicationDataCollection[1].Buffer);
                Assert.AreEqual(outText, testFileContents);
                Assert.AreEqual("html", applicationDataCollection[0].Type);
                Assert.AreEqual(htmlContent, applicationDataCollection[0].HtmlString);
            }
        }

        [TestMethod]
        public void GetAllApplicationAttachmentsTest()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            List<Attachment> attachmentArray = service.GetApplicationAttachments(application);
            Assert.AreEqual(5, attachmentArray.Count);
        }

        [TestMethod]
        public void ApplicationFormViewTest()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            string templateFilePath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\..\DOL.WHD.Section14c.API\App_Data\Section14cApplicationPdfView.html"));
            string template = File.ReadAllText(templateFilePath);
            string applicationFormHtmlContent = service.GetApplicationFormViewContent(application, template);
            Assert.IsNotNull(applicationFormHtmlContent);
        }
    }
}
