using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Net;
using System.Web.Http.Controllers;
using Moq;
using System.Web.Mvc; 
using System.Web.Routing;
using System.Net.Http;
using System.Text;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.Log.ActionFilters.Tests
{
    [TestClass()]
    public class LoggingFilterAttributeTests
    {

        [TestMethod()]
        public void LoggingFilterAttribute_AllowMultipleTest()
        {
            LoggingFilterAttribute actionFilter = new LoggingFilterAttribute();

            Assert.IsTrue(actionFilter.AllowMultiple);            
        }

        [TestMethod()]
        [ExpectedException(typeof(ApiException),
           "Internal Server Error.")]
        public void LoggingFilterAttribute_OnActionExecutingTest()
        {    
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "mywebapi/test");
            httpRequestMessage.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

            var httpActionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext
                {
                    Request = httpRequestMessage,
                    ControllerDescriptor = new HttpControllerDescriptor()
                },
                Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Content = new StringContent("tets")
                }

            };

            var filter = new LoggingFilterAttribute();
            filter.OnActionExecuting(httpActionContext);

            Assert.IsTrue(httpActionContext.Response.StatusCode == HttpStatusCode.Conflict);


        }
    }
}