using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
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
            var save = service.GetSave("CE7F5AA5-6832-47FE-BAE1-80D14CD8F667");

            // Assert
            Assert.AreEqual("{ \"name\": \"Barack Obama\", \"email:\" \"president@whitehouse.gov\" }", save.ApplicationState);
        }

        [TestMethod]
        public void AddsSave()
        {
            // Arrange
            var newData = new ApplicationSave
            {
                Id = "30-9876543",
                ApplicationId = "CE7F5AA5-6832-43FE-BAE1-80D14CD8F666",
                ApplicationState = "{ \"name\": \"Joe Biden\", \"email:\" \"vice.president@whitehouse.gov\" }"
            };

            var service = new SaveService(_saveRepositoryMock);

            // Act
            service.AddOrUpdate(newData.Id, newData.ApplicationId, null, newData.ApplicationState);
            var save = service.GetSave(newData.ApplicationId);

            // Assert
            Assert.AreEqual(newData.ApplicationState, save.ApplicationState);
        }

        [TestMethod]
        public void UpdatesSave()
        {
            // Arrange
            var einToTest = "30-9876543";
            var applicationId = "CE7F5AA5-6832-43FE-BAE1-80D14CD8F666";
            var oldData = new
            {
                EIN = einToTest,
                ApplicationId = "CE7F5AA5-6832-43FE-BAE1-80D14CD8F666",
                ApplicationState = "{ \"name\": \"Joe Biden\", \"email:\" \"vice.president@whitehouse.gov\" }"
            };
            var newData = new
            {
                EIN = einToTest,
                ApplicationId = "CE7F5AA5-6832-43FE-BAE1-80D14CD8F666",
                ApplicationState = "{ \"name\": \"Michelle Obama\", \"email:\" \"first.lady@whitehouse.gov\" }"
            };

            var service = new SaveService(_saveRepositoryMock);
            service.AddOrUpdate(einToTest, applicationId, null, oldData.ApplicationState);
            var existingRecord = service.GetSave(applicationId);

            // Act
            service.AddOrUpdate(einToTest, applicationId, null, newData.ApplicationState);
            var newRecord = service.GetSave(applicationId);

            // Assert
            Assert.AreEqual(newData.ApplicationState, newRecord.ApplicationState);
            Assert.AreEqual(applicationId, newRecord.ApplicationId);
        }

        [TestMethod]
        public void RemovesSave()
        {
            // Arrange
            var applicationId = "CE7F5AA5-6832-47FE-BAE1-80D14CD8F667";
            var service = new SaveService(_saveRepositoryMock);
            var getSave = service.GetSave(applicationId);
            Assert.IsNotNull(getSave);

            // Act
            service.Remove(applicationId);
            getSave = service.GetSave(applicationId);

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
