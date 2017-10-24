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
 
        [TestMethod]
        public void ReturnsAllAttachmentSupportedFileTypes()
        {
            // Arrange
            var service = new AttachmentSupportedFileTypesService();

            // Act
            var allAttachmentSupportedFileTypes = service.GetAllSupportedFileTypes();

            // Assert
            Assert.IsNotNull(allAttachmentSupportedFileTypes);
            Assert.AreEqual(allAttachmentSupportedFileTypes.Cast<string>().First(), "doc");
        }
    }
}
