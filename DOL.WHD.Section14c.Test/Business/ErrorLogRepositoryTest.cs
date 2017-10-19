using System;
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
    public class ErrorLogRepositoryTest
    {
        private readonly IErrorLogRepository _errorLogRepository;
        private readonly LogDetails _data; 

        public ErrorLogRepositoryTest()
        {
            _errorLogRepository = new ErrorLogRepositoryMock();
            _data = new LogDetails { EIN = "22-1234567", Exception = "My Test Exception 22", Level = "INFO", Message = "This a test message", User = "test@test.com", UserId = "123456" };
        }

        [TestMethod]
        public void ActivityLog_ReturnsLogById()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);

            // Act
            var result = service.GetErrorLogByID(2) as OkNegotiatedContentResult<APIErrorLogs>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("12-2234567", result.Content.EIN);
        }

        [TestMethod]
        public void ActivityLog_ReturnsLogs_Invalid()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);

            // Act
            var result = service.GetErrorLogByID(100);

            // Assert
            //Assert.IsNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ActivityLog_ReturnsAllLogs()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);

            // Act
            var result = service.GetAllLogs();

            // Assert
            Assert.AreEqual(6, result.Count());
        }

        [TestMethod]
        public void ActivityLog_AddLogs()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);

            // Act
            var result = service.AddLog(_data) as OkNegotiatedContentResult<LogDetails>; ;

            // Assert
            Assert.AreEqual(_data.EIN, result.Content.EIN);
        }

        [TestMethod]
        public void ActivityLog_Dispose()
        {
            var service = new ErrorLogsController(_errorLogRepository);
            service.Dispose();
            Assert.IsTrue(((ErrorLogRepositoryMock)_errorLogRepository).Disposed);
        }
    }
}
