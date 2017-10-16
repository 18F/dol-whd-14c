using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace DOL.WHD.Section14c.Log.LogHelper
{
    public class BaseApiController : ApiController
    {
        protected void NotFound(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.NotFound, message, HttpStatusCode.NotFound, "");
        }

        protected void BadRequest(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.BadRequest, message, HttpStatusCode.BadRequest, "");
        }
    }
}