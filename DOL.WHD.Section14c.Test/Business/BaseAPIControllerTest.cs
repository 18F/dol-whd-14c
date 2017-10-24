using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.LogHelper;

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
        [ExpectedException(typeof(ApiBusinessException),
           "Error.")]
        public void BaseApiController_BadRequest_Test()
        {
            BadRequest("Bad Request");
        }

        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
           "Error.")]
        public void BaseApiController_Conflict_Test()
        {
            Conflict("Conflict");
        }
        [TestMethod]
        [ExpectedException(typeof(ApiException),
           "Error.")]
        public void BaseApiController_InternalServerError_Test()
        {
            InternalServerError("Internal Server Error");
        }
        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
           "Error.")]
        public void BaseApiController_Unauthorized_Test()
        {
            Unauthorized("Unauthorized");
        }
        [TestMethod]
        [ExpectedException(typeof(ApiDataException),
           "Error.")]
        public void BaseApiController_ExpectationFailed_Test()
        {
            ExpectationFailed("Expectation Failed");
        }
    }
}
