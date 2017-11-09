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

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class AttachmentServiceTests
    {
        private readonly IFileRepository _fileRepositoryMock;
        private readonly IAttachmentRepository _attachmentRepositoryMock;
        public AttachmentServiceTests()
        {
            _fileRepositoryMock = new FileRepository(@"TestUploads\");
            _attachmentRepositoryMock = new AttachmentRepositoryMock();
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
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

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
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

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
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

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
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

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
            service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

            service.DeleteAttachement(einToTest, Guid.Empty);
        }        

        [TestMethod]
        public void Dispose()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            service.Dispose();
        }
    }
}
