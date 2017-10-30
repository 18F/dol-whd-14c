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
    public class ErrorLogsControllerTest
    {
        private readonly IErrorLogRepository _errorLogRepository;
        private readonly LogDetails _data;

        public ErrorLogsControllerTest()
        {
            _errorLogRepository = new ErrorLogRepositoryMock();
            _data = new LogDetails { EIN = "22-1234567", Exception = "My Test Exception 22", Level = "INFO", Message = "This a test message", User = "test@test.com", UserId = "123456" };
        }

        [TestInitialize]
        public void Initialize()
        {
            ((ErrorLogRepositoryMock)_errorLogRepository).AddShouldFail = false;
        }

        [TestMethod]
        public void ErrorLog_ReturnsLogById()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);

            // Act
            var result = service.GetErrorLogByID("2edbc12f-4fd8-4fed-a848-b8bfff4d4e03") as OkNegotiatedContentResult<APIErrorLogs>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("12-2234567", result.Content.EIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
            "Log not found.")]
        public void ErrorLog_ReturnsLogById_Invalid()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);
            // Act
            var result = service.GetErrorLogByID("edbc12f-4fd8-4fed-a848-b8bff");
            
        }

        [TestMethod]
        public void ErrorLog_ReturnsAllLogs()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);

            // Act
            var result = service.GetAllLogs();

            // Assert
            Assert.AreEqual(6, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
            "Log not found.")]
        public void ErrorLog_ReturnsAllLogs_Invalid()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);
            ((ErrorLogRepositoryMock)_errorLogRepository).AddShouldFail = true;

            // Act
            var result = service.GetAllLogs();            
        }

        [TestMethod]
        public void ErrorLog_AddLogs()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);

            // Act
            var result = service.AddLog(_data) as OkNegotiatedContentResult<LogDetails>; ;

            // Assert
            Assert.AreEqual(_data.EIN, result.Content.EIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
            "Log not found.")]
        public void ErrorLog_Addlog_Invalid()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);
           
            ((ErrorLogRepositoryMock)_errorLogRepository).AddShouldFail = true;

            // Act
            var result = service.AddLog(_data) as OkNegotiatedContentResult<LogDetails>;
        }

        [TestMethod]
        [ExpectedException(typeof(ApiBusinessException),
           "Model State is not valid.")]
        public void ErrorLog_Addlog_InvalidModelState()
        {
            // Arrange
            var service = new ErrorLogsController(_errorLogRepository);
            service.ModelState.AddModelError("key", "error message");
            // Act
            var result = service.AddLog(_data) as OkNegotiatedContentResult<LogDetails>;
        }        

        [TestMethod]
        public void ErrorLog_Dispose()
        {
            var service = new ErrorLogsController(_errorLogRepository);
            service.Dispose();
            Assert.IsTrue(((ErrorLogRepositoryMock)_errorLogRepository).Disposed);
        }
    }
}
