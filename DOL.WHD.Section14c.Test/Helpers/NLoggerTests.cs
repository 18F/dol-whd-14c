
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Tracing;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Net;

namespace DOL.WHD.Section14c.Log.Helpers.Tests
{
    [TestClass()]
    public class NLoggerTests
    {
        [TestMethod()]
        public void NLogger_TraceTest()
        {
            var correlationId = Guid.NewGuid().ToString();

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
                    Content = new StringContent("test")
                }

            };
            var mockLogger = new LoggerMock();
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), mockLogger);
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            httpActionContext.Request.Properties[Constants.CorrelationId] = correlationId;
            trace.Info(httpActionContext.Request,
                "Category",
                "JSON", 
                "ActionArguments");


            Assert.AreEqual("Category", mockLogger.LogToOperationsCalledWithMessage);

        }
    }
}