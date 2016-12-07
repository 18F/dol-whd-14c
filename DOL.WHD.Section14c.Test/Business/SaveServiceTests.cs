using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class SaveServiceTests
    {
        private readonly ISaveRepository _saveRepositoryMock;
        private readonly IFileRepository _fileRepositoryMock;

        public SaveServiceTests()
        {
            _saveRepositoryMock = new SaveRepositoryMock();
            _fileRepositoryMock = new FileRepository(@"TestUploads\");
        }

        [TestMethod]
        public void RetrievesSave()
        {
            // Arrange
            var service = new SaveService(_saveRepositoryMock, null);

            // Act
            var save = service.GetSave("30-1234567");

            // Assert
            Assert.AreEqual("{ \"name\": \"Barack Obama\", \"email:\" \"president@whitehouse.gov\" }", save.ApplicationState);
        }

        [TestMethod]
        public void AddsSave()
        {
            // Arrange
            var newData = new ApplicationSave
            {
                EIN = "30-9876543",
                ApplicationState = "{ \"name\": \"Joe Biden\", \"email:\" \"vice.president@whitehouse.gov\" }"
            };

            var service = new SaveService(_saveRepositoryMock, null);

            // Act
            service.AddOrUpdate(newData.EIN, newData.ApplicationState);
            var save = service.GetSave(newData.EIN);

            // Assert
            Assert.AreEqual(newData.ApplicationState, save.ApplicationState);
        }

        [TestMethod]
        public void UpdatesSave()
        {
            // Arrange
            var einToTest = "30-9876543";
            var oldData = new
            {
                EIN = einToTest,
                ApplicationState = "{ \"name\": \"Joe Biden\", \"email:\" \"vice.president@whitehouse.gov\" }"
            };
            var newData = new
            {
                EIN = einToTest,
                ApplicationState = "{ \"name\": \"Michelle Obama\", \"email:\" \"first.lady@whitehouse.gov\" }"
            };

            var service = new SaveService(_saveRepositoryMock, null);
            service.AddOrUpdate(einToTest, oldData.ApplicationState);
            var existingRecord = service.GetSave(einToTest);

            // Act
            service.AddOrUpdate(einToTest, newData.ApplicationState);
            var newRecord = service.GetSave(einToTest);

            // Assert
            Assert.AreEqual(newData.ApplicationState, newRecord.ApplicationState);
            Assert.AreEqual(einToTest, newRecord.EIN);
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

            var service = new SaveService(_saveRepositoryMock, _fileRepositoryMock);
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

            using (var outMemoryStream = new MemoryStream())
            {
                service.DownloadAttachment(outMemoryStream, einToTest, upload.Id);

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

            var service = new SaveService(_saveRepositoryMock, _fileRepositoryMock);

            // Act
            service.AddOrUpdate(newData.EIN, newData.ApplicationState);
            service.GetSave(newData.EIN);

            
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

            using (var outMemoryStream = new MemoryStream())
            {
                service.DownloadAttachment(outMemoryStream, einToTest, upload.Id);

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

            var service = new SaveService(_saveRepositoryMock, _fileRepositoryMock);
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

            var service = new SaveService(_saveRepositoryMock, _fileRepositoryMock);
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

            var existingObj = _saveRepositoryMock.Get().FirstOrDefault(x => x.EIN == einToTest).Attachments.FirstOrDefault();
            existingObj.RepositoryFilePath = "invalidPath";
            _saveRepositoryMock.SaveChanges();

            using (var outMemoryStream = new MemoryStream())
            {
                service.DownloadAttachment(outMemoryStream, einToTest, upload.Id);

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

            var service = new SaveService(_saveRepositoryMock, _fileRepositoryMock);
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

            service.DeleteAttachement(einToTest, upload.Id);

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

            var service = new SaveService(_saveRepositoryMock, _fileRepositoryMock);
            var upload = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");

            service.DeleteAttachement(einToTest, Guid.Empty);
        }

        [TestMethod]
        public void Dispose()
        {
            var service = new SaveService(_saveRepositoryMock, _fileRepositoryMock);
            service.Dispose();
        }

    }
}
