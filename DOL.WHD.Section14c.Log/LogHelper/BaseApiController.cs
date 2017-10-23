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
        /// <summary>
        /// Throw Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        public void NotFound(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.NotFound, message, HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Throw Bad Request Exception
        /// </summary>
        /// <param name="message"></param>
        public void BadRequest(string message)
        {
            throw new ApiBusinessException((int)HttpStatusCode.BadRequest, message, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Throw Conflict Exception
        /// </summary>
        /// <param name="message"></param>
        public void Conflict(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.Conflict, message, HttpStatusCode.Conflict);
        }

        /// <summary>
        /// Throw Internal Server Error Exception
        /// </summary>
        /// <param name="message"></param>
        public void InternalServerError(string message)
        {
            throw new ApiException((int)HttpStatusCode.InternalServerError, message, HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Throw Unauthorized Exception
        /// </summary>
        /// <param name="message"></param>
        public void Unauthorized(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.Unauthorized, message, HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Throw ExpectationFailed Exception
        /// </summary>
        /// <param name="message"></param>
        public void ExpectationFailed(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.ExpectationFailed, message, HttpStatusCode.ExpectationFailed);
        }
    }
}