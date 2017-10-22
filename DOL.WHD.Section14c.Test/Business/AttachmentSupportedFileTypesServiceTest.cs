using System.Linq;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class AttachmentSupportedFileTypesServiceTest
    {        

        private readonly IAttachmentSupportedFileTypesRepository _attachmentSupportedFileTypesRepositoryMock;

        public AttachmentSupportedFileTypesServiceTest()
        {
            _attachmentSupportedFileTypesRepositoryMock = new AttachmentSupportedFileTypesRepositoryMock();
        }

        [TestMethod]
        public void ReturnsAttachmentSupportedFileTypes()
        {
            // Arrange
            var service = new AttachmentSupportedFileTypesService(_attachmentSupportedFileTypesRepositoryMock);

            // Act
            var statusObj = service.GetSupportedFileTypes(1);

            // Assert
            Assert.AreEqual("doc", statusObj.Name);
        }

        [TestMethod]
        public void ReturnsAttachmentSupportedFileTypes_Invalid()
        {
            // Arrange
            var service = new AttachmentSupportedFileTypesService(_attachmentSupportedFileTypesRepositoryMock);

            // Act
            var statusObj = service.GetSupportedFileTypes(100);

            // Assert
            Assert.IsNull(statusObj);
        }

        [TestMethod]
        public void ReturnsAllAttachmentSupportedFileTypes()
        {
            // Arrange
            var service = new AttachmentSupportedFileTypesService(_attachmentSupportedFileTypesRepositoryMock);

            // Act
            var allAttachmentSupportedFileTypes = service.GetAllSupportedFileTypes();

            // Assert
            Assert.AreEqual(8, allAttachmentSupportedFileTypes.Count());
        }

        [TestMethod]
        public void Dispose()
        {
            var service = new AttachmentSupportedFileTypesService(_attachmentSupportedFileTypesRepositoryMock);
            service.Dispose();
            Assert.IsTrue(((AttachmentSupportedFileTypesRepositoryMock)_attachmentSupportedFileTypesRepositoryMock).Disposed);
        }
    }
}
