using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ReCaptphaTests
    {
        private const string TestKey = "key";
        private const string TestResponse = "response";
        private const string TestIpAddress = "127.0.0.1";

        [TestMethod]
        public void ValidateResponse_DisabledNull()
        {
            var mock = new Mock<IRestClient>();
            mock.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse { Content = "{success:true}"});

            var client = new ReCaptchaService(mock.Object);
            var response = client.ValidateResponse(null, TestResponse, TestIpAddress);

            Assert.IsTrue(response == ReCaptchaValidationResult.Disabled);
        }

        [TestMethod]
        public void ValidateResponse_DisabledEmpty()
        {
            var mock = new Mock<IRestClient>();
            mock.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse { Content = "{success:true}" });

            var client = new ReCaptchaService(mock.Object);
            var response = client.ValidateResponse(string.Empty, TestResponse, TestIpAddress);

            Assert.IsTrue(response == ReCaptchaValidationResult.Disabled);
        }

        [TestMethod]
        public void ValidateResponse_InvalidResponse()
        {
            var mock = new Mock<IRestClient>();
            mock.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse { Content = "{success:false}" });

            var client = new ReCaptchaService(mock.Object);
            var response = client.ValidateResponse(TestKey, TestResponse, TestIpAddress);

            Assert.IsTrue(response == ReCaptchaValidationResult.InvalidResponse);
        }

        [TestMethod]
        public void ValidateResponse_InvalidResponseWithError()
        {
            var mock = new Mock<IRestClient>();
            mock.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse { ResponseStatus = ResponseStatus.Error });

            var client = new ReCaptchaService(mock.Object);
            var response = client.ValidateResponse(TestKey, TestResponse, TestIpAddress);

            Assert.IsTrue(response == ReCaptchaValidationResult.InvalidResponse);
        }

    }
}
