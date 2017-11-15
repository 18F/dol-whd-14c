
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
using Moq;
using NLog;

namespace DOL.WHD.Section14c.Log.Helpers.Tests
{
    [TestClass()]
    public class NLoggerTests
    {
        [TestMethod()]
        public void NLogger_TraceTest()
        {
            try
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

                var loggerMock = new Mock<ILogger>();

                // Create Mock for NLog
                // NLog is unit tested in NLog itself. No need to retest it.
                // If there is no exception, test consider passed
                GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger(loggerMock.Object));
                var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                httpActionContext.Request.Properties[Constants.CorrelationId] = correlationId;
                trace.Info(httpActionContext.Request,
                    "Category",
                    "JSON",
                    "ActionArguments");
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }
    }
}
