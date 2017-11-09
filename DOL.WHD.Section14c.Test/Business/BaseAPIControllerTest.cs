using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.LogHelper;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Web;
using Moq;
using System.Net;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class BaseAPIControllerTest : BaseApiController
    {
        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
           "Error.")]
        public void BaseApiController_NotFound_Test()
        {
            NotFound("Not Found");
        }

        [TestMethod]
        [ExpectedException(typeof(ApiBusinessException))]
        public void BaseApiController_BadRequest_Test()
        {
            BadRequest("Bad Request");
        }

        [TestMethod]
        [ExpectedException(typeof(ApiDataException))]
        public void BaseApiController_Conflict_Test()
        {
            Conflict("Conflict");
        }
        [TestMethod]
        [ExpectedException(typeof(ApiException))]
        public void BaseApiController_InternalServerError_Test()
        {
            InternalServerError("Internal Server Error");
        }
        [TestMethod]
        [ExpectedException(typeof(ApiDataException))]
        public void BaseApiController_Unauthorized_Test()
        {
            Unauthorized("Unauthorized");
        }
        [TestMethod]
        [ExpectedException(typeof(ApiDataException))]
        public void BaseApiController_ExpectationFailed_Test()
        {
            ExpectationFailed("Expectation Failed");
        }

        [TestMethod()]
        public void MyHttpClientTest()
        {
            var httpClientInstance = MyHttpClient;
            Assert.IsNotNull(httpClientInstance);
        }

        [TestMethod()]
        public void DownloadTest()
        {
            // Arrange
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.pdf";

            var request = new Mock<HttpRequestMessage>();
            HttpResponseMessage response = request.Object.CreateResponse(HttpStatusCode.OK);
            var resp = Download(data, response, fileName);
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ObjectDisposedException))]
        public void Download_ExpectationFailed_Test()
        {
            // Arrange
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.pdf";

            var request = new Mock<HttpRequestMessage>();
            HttpResponseMessage response = request.Object.CreateResponse(HttpStatusCode.OK);
            response.Dispose();
            var resp = Download(data, response, fileName);
        }
    }
}
