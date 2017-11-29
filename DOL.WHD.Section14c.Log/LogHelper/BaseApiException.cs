using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Net;
using DOL.WHD.Section14c.Log.Helpers;

namespace DOL.WHD.Section14c.Log.LogHelper
{
    /// <summary>
    /// Basic API exception, from which more specific exception types may derive
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class BaseApiException: Exception
    {
        //#region Public Serializable properties.
        /// <summary>
        /// Error code
        /// </summary>
        [DataMember]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Description of the error
        /// </summary>
        [DataMember]
        public string ErrorDescription { get; set; }

        /// <summary>
        /// HTTP status that best represents the error
        /// </summary>
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        /// <summary>
        /// Reason for the error
        /// </summary>
        protected string reasonPhrase = string.Empty;

        /// <summary>
        /// Reason for the error
        /// </summary>
        [DataMember]
        public virtual string ReasonPhrase
        {
            get { return this.reasonPhrase; }
        }

        /// <summary>
        /// Public constructor for Api Data Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        public BaseApiException(int errorCode, string errorDescription, HttpStatusCode httpStatus)
            : this(errorCode, errorDescription, httpStatus, null)
        {
            
        }

        /// <summary>
        /// Public constructor for Api Data Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        /// <param name="inner"></param>
        public BaseApiException(int errorCode, string errorDescription, HttpStatusCode httpStatus, Exception inner) 
            : base(errorDescription, inner)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
            HttpStatus = httpStatus;           
        }
    }
}