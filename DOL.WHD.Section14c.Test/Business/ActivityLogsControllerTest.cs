using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.DataAccess.Repositories;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using DOL.WHD.Section14c.Log.Controllers;
using DOL.WHD.Section14c.Log.Models;
using System.Linq;
using System.Web.Http.Results;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class ActivityLogsControllerTest
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public ActivityLogsControllerTest()
        {
            _activityLogRepository = new ActivityLogRepositoryMock();
        }

        [TestInitialize]
        public void Initialize()
        {
            ((ActivityLogRepositoryMock)_activityLogRepository).AddShouldFail = false;
        }

        [TestMethod]
        public void ActivityLog_ReturnsLogById()
        {
            // Arrange
            var service = new ActivityLogsController(_activityLogRepository);

            // Act
            var result = service.GetActivityLogByID("2edbc12f-4fd8-4fed-a848-b8bfff4d4e03") as OkNegotiatedContentResult<APIActivityLogs>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("12-2234567", result.Content.EIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
            "Log not found.")]
        public void ActivityLog_ReturnsLogById_Invalid()
        {
            // Arrange
            var service = new ActivityLogsController(_activityLogRepository);
            // Act
            var result = service.GetActivityLogByID("100");
        }

        [TestMethod]
        public void ActivityLog_ReturnsAllLogs()
        {
            // Arrange
            var service = new ActivityLogsController(_activityLogRepository);

            // Act
            var result = service.GetAllLogs();

            // Assert
            Assert.AreEqual(6, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
            "Log not found.")]
        public void ActivityLog_ReturnsAllLogs_Invalid()
        {
            // Arrange
            var service = new ActivityLogsController(_activityLogRepository);
            ((ActivityLogRepositoryMock)_activityLogRepository).AddShouldFail = true;

            // Act
            var result = service.GetAllLogs();            
        }

        [TestMethod]
        public void ActivityLog_Dispose()
        {
            var service = new ActivityLogsController(_activityLogRepository);
            service.Dispose();
            Assert.IsTrue(((ActivityLogRepositoryMock)_activityLogRepository).Disposed);
        }
    }
}
