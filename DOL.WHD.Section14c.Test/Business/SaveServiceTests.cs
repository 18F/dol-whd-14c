using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Identity;
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

            var service = new SaveService(_saveRepositoryMock);

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

            var service = new SaveService(_saveRepositoryMock);
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
        public void RemovesSave()
        {
            // Arrange
            var ein = "30-1234567";
            var service = new SaveService(_saveRepositoryMock);
            var getSave = service.GetSave(ein);
            Assert.IsNotNull(getSave);

            // Act
            service.Remove(ein);
            getSave = service.GetSave(ein);

            // Assert
            Assert.IsNull(getSave);
        }

        [TestMethod]
        public void Dispose()
        {
            var service = new SaveService(_saveRepositoryMock);
            service.Dispose();
        }

    }
}
