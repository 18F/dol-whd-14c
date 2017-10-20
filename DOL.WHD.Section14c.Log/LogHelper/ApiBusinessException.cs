using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace DOL.WHD.Section14c.Log.LogHelper
{ /// <summary>
  /// Api Business Exception
  /// </summary>
    [Serializable]
    [DataContract]
    public class ApiBusinessException : BaseApiException
    {

        #region Public Constructor.
        /// <summary>
        /// Public constructor for Api Business Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        public ApiBusinessException(int errorCode, string errorDescription, HttpStatusCode httpStatus, string correlationId)
            : base(errorCode, errorDescription, httpStatus, correlationId)
        {
            reasonPhrase = "API Business Exception";
        }
        #endregion

    }
}