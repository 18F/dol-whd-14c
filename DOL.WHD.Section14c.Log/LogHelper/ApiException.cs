using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace DOL.WHD.Section14c.Log.LogHelper
{
    /// <summary>
    /// Api Exception
    /// </summary>
    [Serializable]
    [DataContract]
    public class ApiException : BaseApiException
    {
        
        /// <summary>
        /// Public constructor for Api Data Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        /// <param name="correlationId"></param>
        public ApiException(int errorCode, string errorDescription, HttpStatusCode httpStatus, string correlationId)
            :base(errorCode, errorDescription, httpStatus, correlationId)
        {            
        }
        /// <summary>
        /// Public constructor for Api Data Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        /// <param name="correlationId"></param>
        /// <param name="inner"></param>
        public ApiException(int errorCode, string errorDescription, HttpStatusCode httpStatus, string correlationId, Exception inner)
            : base(errorCode, errorDescription, httpStatus, correlationId, inner)
        {            
        }

        //#endregion
    }
}