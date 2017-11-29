using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net;
using Moq;
using System.Web.Http;
using DOL.WHD.Section14c.Log.LogHelper;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Log.ActionFilters.Tests
{
    [TestClass()]
    public class GlobalExceptionAttributeTests
    {
        private GlobalExceptionAttribute filter;
        private HttpActionExecutedContext httpActionExecutedContext;

        [TestInitialize]
        public void Initialize()
        {

            filter = new GlobalExceptionAttribute();
            var configuration = new HttpConfiguration();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "mywebapi/test");
            httpRequestMessage.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");
            httpRequestMessage.RequestUri = new Uri("http://localhost:8080/addlog/test");
            httpRequestMessage.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

            
            string controllerName = "UsersController";
            Type controllerType = typeof(BaseApiController);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Conflict,
                Content = new StringContent("test")
            };

            httpActionExecutedContext = new HttpActionExecutedContext
            {
                ActionContext = new HttpActionContext
                {
                    ActionDescriptor = new Mock<HttpActionDescriptor>() { CallBase = true }.Object,
                    ControllerContext = new HttpControllerContext
                    {
                        Configuration = new HttpConfiguration(new HttpRouteCollection("api")),
                        Request = httpRequestMessage,
                        RequestContext = new HttpRequestContext(),
                        ControllerDescriptor = new HttpControllerDescriptor(configuration, controllerName, controllerType)
                    },
                    Response = response,                    
                },
                Response = response
            };         
        }

        [TestMethod()]
        public void GlobalExceptionAttribute_AllowMultipleTest()
        {
            GlobalExceptionAttribute actionFilter = new GlobalExceptionAttribute();

            Assert.IsTrue(actionFilter.AllowMultiple);            
        }


        [TestMethod()]
        public void GlobalExceptionAttribute_OnExceptionTest_ValidationException()
        {
            try
            {
                httpActionExecutedContext.Exception = new ValidationException();
                filter.OnException(httpActionExecutedContext);
            }
            catch(HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.BadRequest);
            }
        }


       [TestMethod()]
        public void GlobalExceptionAttribute_OnExceptionTest_UnauthorizedAccessException()
        {
            try
            {
                httpActionExecutedContext.Exception = new UnauthorizedAccessException();
                filter.OnException(httpActionExecutedContext);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.Unauthorized);
            }
        }

        [TestMethod()]
        public void GlobalExceptionAttribute_OnExceptionTest_ApiException()
        {
            try
            {
                httpActionExecutedContext.Exception = new ApiException(
                                    (int)HttpStatusCode.InternalServerError, 
                                    "Error", 
                                    HttpStatusCode.InternalServerError );
                filter.OnException(httpActionExecutedContext);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.InternalServerError);
            }
        }

        [TestMethod()]
        public void GlobalExceptionAttribute_OnExceptionTest_ApiBusinessException()
        {
            try { 
                httpActionExecutedContext.Exception = new ApiBusinessException(
                    (int)HttpStatusCode.BadRequest,
                    "Error",
                    HttpStatusCode.BadRequest);
                filter.OnException(httpActionExecutedContext);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.BadRequest);
            }
        }

        [TestMethod()]
        public void GlobalExceptionAttribute_OnExceptionTest_ApiDataException()
        {
            try { 
                httpActionExecutedContext.Exception = new ApiDataException(
                    (int)HttpStatusCode.NotFound,
                    "Error",
                    HttpStatusCode.NotFound);
                filter.OnException(httpActionExecutedContext);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.NotFound);
            }
        }

        [TestMethod()]
        public void GlobalExceptionAttribute_OnExceptionTest_Exception()
        {
            try { 
                httpActionExecutedContext.Exception = new Exception();
                filter.OnException(httpActionExecutedContext);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.InternalServerError);
            }
        }
    }
}