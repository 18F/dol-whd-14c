using DOL.WHD.Section14c.Log.ActionFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Net;
using System.Web.Http.Controllers;
using Moq;
using System.Net.Http;
using System.Text;
using DOL.WHD.Section14c.Log.LogHelper;
using System.Web.Http;
using System;

namespace DOL.WHD.Section14c.Log.ActionFilters.Tests
{
    [TestClass()]
    public class LoggingFilterAttributeTests
    {
        private HttpActionContext httpActionContext;
        private LoggingFilterAttribute filter;

        [TestInitialize]
        public void Initialize()
        {
            var httpRequestMessage = new Mock<HttpRequestMessage>();
            filter = new LoggingFilterAttribute();

            HttpConfiguration config = new HttpConfiguration();
            string controllerName = "UsersController";
            Type controllerType = typeof(BaseApiController);

            var httpActionDescriptor = new Mock<HttpActionDescriptor>();
            httpActionDescriptor.Name = "Test";

            httpActionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext
                {
                    Request = httpRequestMessage.Object as HttpRequestMessage,
                    ControllerDescriptor = new HttpControllerDescriptor(config, controllerName, controllerType)
                },
                Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Content = new StringContent("test")
                },
                ActionDescriptor = httpActionDescriptor.Object as HttpActionDescriptor
            };
        }


        [TestMethod()]
        public void LoggingFilterAttribute_AllowMultipleTest()
        {
            LoggingFilterAttribute actionFilter = new LoggingFilterAttribute();

            Assert.IsTrue(actionFilter.AllowMultiple);
        }

        [TestMethod()]
        public void LoggingFilterAttribute_OnActionExecutingTest()
        {

            filter.OnActionExecuting(httpActionContext);
        }

        [TestMethod()]
        [ExpectedException(typeof(ApiException),
           "Internal Server Error.")]
        public void LoggingFilterAttribute_OnActionExecutingTest_Invalid()
        {
            filter.OnActionExecuting(null);            
        }

    }
}