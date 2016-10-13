using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class SaveServiceTests
    {
        private readonly ISaveRepository _saveRepositoryMock;

        public SaveServiceTests()
        {
            _saveRepositoryMock = new SaveRepositoryMock();
        }

        [TestMethod]
        public void RetrievesSave()
        {
            // Arrange
            var service = new SaveService(_saveRepositoryMock);

            // Act
            var save = service.GetSave("1", "30-1234567");

            // Assert
            Assert.AreEqual("{ \"name\": \"Barack Obama\", \"email:\" \"president@whitehouse.gov\" }", save);
        }

        [TestMethod]
        public void AddsSave()
        {
            // Arrange
            var newData = new ApplicationSave
            {
                UserId = "2",
                EIN = "30-9876543",
                ApplicationState = "{ \"name\": \"Joe Biden\", \"email:\" \"vice.president@whitehouse.gov\" }"
            };

            var service = new SaveService(_saveRepositoryMock);

            // Act
            service.AddOrUpdate(newData.UserId, newData.EIN, newData.ApplicationState);
            var save = service.GetSave(newData.UserId, newData.EIN);

            // Assert
            Assert.AreEqual(newData.ApplicationState, save);
        }
    }
}
