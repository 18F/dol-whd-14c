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
    public class ApiException : Exception, IApiExceptions
    {
        #region Public Serializable properties.
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }
        [DataMember]
        public string CorrelationId { get; set; }

        string reasonPhrase = "API Exception";

        [DataMember]
        public string ReasonPhrase
        {
            get { return this.reasonPhrase; }

            set { this.reasonPhrase = value; }
        }


        /// <summary>
        /// Public constructor for Api Data Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        public ApiException(int errorCode, string errorDescription, HttpStatusCode httpStatus, string correlationId)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
            HttpStatus = httpStatus;
            CorrelationId = correlationId;
        }
        #endregion
    }
}