using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace DOL.WHD.Section14c.Log.LogHelper
{
    public class BaseApiController : ApiController
    {
        protected NotFoundResult NotFound(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.NotFound, message, HttpStatusCode.NotFound, "");
            return null;
        }

        protected BadRequestResult BadRequest(string message)
        {
            throw new ApiBusinessException((int)HttpStatusCode.BadRequest, message, HttpStatusCode.BadRequest, "");
            return null;
        }

        protected BadRequestResult BadRequest(string message, Exception ex)
        {
            throw new ApiBusinessException((int)HttpStatusCode.BadRequest, ex.Message, HttpStatusCode.BadRequest, "");

            return null;
        }
        
        protected ConflictResult Conflict(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.Conflict, message, HttpStatusCode.Conflict, "");
            return null;
        }
        
        protected InternalServerErrorResult InternalServerError(string message)
        {
            throw new ApiException((int)HttpStatusCode.InternalServerError, message, HttpStatusCode.InternalServerError, "");
            return null;
        }

        protected UnauthorizedResult Unauthorized(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.Unauthorized, message, HttpStatusCode.Unauthorized, "");

            return null;
        }

        protected ExceptionResult ExpectationFailed(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.ExpectationFailed, message, HttpStatusCode.ExpectationFailed, "");
            return null;
        }
    }
}