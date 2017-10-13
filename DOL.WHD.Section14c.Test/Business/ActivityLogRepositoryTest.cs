﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.Repositories;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using DOL.WHD.Section14c.Log.Controllers;
using DOL.WHD.Section14c.Log.Models;
using System.Linq;
using System.Web.Http.Results;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ActivityLogRepositoryTest
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public ActivityLogRepositoryTest()
        {
            _activityLogRepository = new ActivityLogRepositoryMock();
        }

        [TestMethod]
        public void ActivityLog_ReturnsLogById()
        {
            // Arrange
            var service = new ActivityLogsController(_activityLogRepository);

            // Act
            var result = service.GetActivityLogByID(2) as OkNegotiatedContentResult<APIActivityLogs>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("12-2234567", result.Content.EIN);
        }

        [TestMethod]
        public void ActivityLog_ReturnsLogs_Invalid()
        {
            // Arrange
            var service = new ActivityLogsController(_activityLogRepository);

            // Act
            var result = service.GetActivityLogByID(100);

            // Assert
            //Assert.IsNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
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
        public void ActivityLog_Dispose()
        {
            var service = new ActivityLogsController(_activityLogRepository);
            service.Dispose();
            Assert.IsTrue(((ActivityLogRepositoryMock)_activityLogRepository).Disposed);
        }
    }
}