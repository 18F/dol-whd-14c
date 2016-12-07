using System.Linq;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class StatusServiceTests
    {
        private readonly IStatusRepository _statusRepositoryMock;

        public StatusServiceTests()
        {
            _statusRepositoryMock = new StatusRepositoryMock();
        }

        [TestMethod]
        public void ReturnsStatus()
        {
            // Arrange
            var service = new StatusService(_statusRepositoryMock);

            // Act
            var statusObj = service.GetStatus(1);

            // Assert
            Assert.AreEqual("Pending", statusObj.Name);
        }

        [TestMethod]
        public void ReturnsStatus_Invalid()
        {
            // Arrange
            var service = new StatusService(_statusRepositoryMock);

            // Act
            var statusObj = service.GetStatus(100);

            // Assert
            Assert.IsNull(statusObj);
        }

        [TestMethod]
        public void ReturnsAllStatuses()
        {
            // Arrange
            var service = new StatusService(_statusRepositoryMock);

            // Act
            var allStatuses = service.GetAllStatuses();

            // Assert
            Assert.AreEqual(7, allStatuses.Count());
        }

        [TestMethod]
        public void Dispose()
        {
            var service = new StatusService(_statusRepositoryMock);
            service.Dispose();
            Assert.IsTrue(((StatusRepositoryMock)_statusRepositoryMock).Disposed);
        }
    }
}
