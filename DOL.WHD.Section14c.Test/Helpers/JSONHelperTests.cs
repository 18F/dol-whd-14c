using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DOL.WHD.Section14c.Log.Controllers;
using System.Web.Http;
using DOL.WHD.Section14c.Log.DataAccess.Repositories;

namespace DOL.WHD.Section14c.Log.Helpers.Tests
{
    [TestClass()]
    public class JSONHelperTests
    {
        private readonly ServiceStatus serviceSTatus;

        public JSONHelperTests()
        {
            serviceSTatus = new ServiceStatus();
            serviceSTatus.CorrelationId = "123";
            serviceSTatus.ReasonPhrase = "This is test";
            serviceSTatus.StatusCode = 123;
            serviceSTatus.StatusMessage = "Test message";
        }

        [TestMethod()]
        public void JSONHelper_ToJSONTest()
        {          
            string result = JSONHelper.ToJSON(serviceSTatus);
            string exceptedJSONValue = "{\"StatusCode\":123,\"StatusMessage\":\"Test message\",\"ReasonPhrase\":\"This is test\",\"CorrelationId\":\"123\"}";
            Assert.AreEqual(result, exceptedJSONValue);
            
        }   
    }
}