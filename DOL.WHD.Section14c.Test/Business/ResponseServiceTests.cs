using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ResponseServiceTests
    {
        private readonly IResponseRepository _responseRepositoryMock;

        public ResponseServiceTests()
        {
            _responseRepositoryMock = new ResponseRepositoryMock();
        }

        [TestMethod]
        public void FiltersByQuestionKey()
        {
            // Arrange
            var service = new ResponseService(_responseRepositoryMock);

            // Act
            var responses = service.GetResponses("EmployerStatus");

            // Assert
            Assert.IsFalse(responses.Any(r => r.QuestionKey != "EmployerStatus"));
        }

        [TestMethod]
        public void FiltersOnlyActive()
        {
            // Arrange
            var service = new ResponseService(_responseRepositoryMock);

            // Act
            var responses = service.GetResponses();

            // Assert
            Assert.IsFalse(responses.Any(r => !r.IsActive));
        }

        [TestMethod]
        public void ReturnsBothActiveAndInactive()
        {
            // Arrange
            var service = new ResponseService(_responseRepositoryMock);

            // Act
            var responses = service.GetResponses(onlyActive: false);

            // Assert
            Assert.IsTrue(responses.Any(r => !r.IsActive));
        }

        [TestMethod]
        public void Dispose()
        {
            var service = new ResponseService(_responseRepositoryMock);
            service.Dispose();
            Assert.IsTrue(((ResponseRepositoryMock)_responseRepositoryMock).Disposed);
        }
    }
}
