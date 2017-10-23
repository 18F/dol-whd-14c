using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Net;
using DOL.WHD.Section14c.Log.Helpers;

namespace DOL.WHD.Section14c.Log.LogHelper
{
    [Serializable]
    [DataContract]
    public abstract class BaseApiException: Exception
    {
        //#region Public Serializable properties.
        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorDescription { get; set; }

        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

              protected string reasonPhrase = string.Empty;

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