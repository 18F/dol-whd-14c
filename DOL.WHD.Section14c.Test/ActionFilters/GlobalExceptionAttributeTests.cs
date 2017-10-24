using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Filters;

namespace DOL.WHD.Section14c.Log.ActionFilters.Tests
{
    [TestClass()]
    public class GlobalExceptionAttributeTests
    {
        [TestMethod()]
        public void GlobalExceptionAttribute_AllowMultipleTest()
        {
            GlobalExceptionAttribute actionFilter = new GlobalExceptionAttribute();

            Assert.IsTrue(actionFilter.AllowMultiple);

            //var request = new HttpRequestMessage();
            //var actionContext = InitializeActionContext(request);
            //var httpActionExectuedContext = new HttpActionExecutedContext(actionContext, ex);

            //var exceptionHandlingAttribute = new GlobalExceptionAttribute();
            //exceptionHandlingAttribute.OnException(httpActionExectuedContext);
            //Assert.Equals(actionContext.Response.StatusCode, statusCode);
            //Assert.Equals(actionContext.Response.ReasonPhrase, ex.Message);
        }
    }
}