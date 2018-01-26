using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.Log.LogHelper
{
    /// <summary>
    /// BaseApiController is used to log exceptions to NLog
    /// </summary>
    public class BaseApiController : ApiController
    {
        public HttpClient MyHttpClient
        {
            get
            {
                ObjectCache cache = MemoryCache.Default;
                if (cache["HttpClient"] == null)
                {
                    cache["HttpClient"] = new HttpClient();
                }
                return (HttpClient)cache["HttpClient"];
            }
        }

        /// <summary>
        /// Throw Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        protected void NotFound(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.NotFound, message, HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Throw Bad Request Exception
        /// </summary>
        /// <param name="message"></param>
        protected void BadRequest(string message)
        {
            throw new ApiBusinessException((int)HttpStatusCode.BadRequest, message, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Throw Conflict Exception
        /// </summary>
        /// <param name="message"></param>
        protected void Conflict(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.Conflict, message, HttpStatusCode.Conflict);
        }

        /// <summary>
        /// Throw Internal Server Error Exception
        /// </summary>
        /// <param name="message"></param>
        protected void InternalServerError(string message)
        {
            throw new ApiException((int)HttpStatusCode.InternalServerError, message, HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Throw Unauthorized Exception
        /// </summary>
        /// <param name="message"></param>
        protected void Unauthorized(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.Unauthorized, message, HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Throw ExpectationFailed Exception
        /// </summary>
        /// <param name="message"></param>
        protected void ExpectationFailed(string message)
        {
            throw new ApiDataException((int)HttpStatusCode.ExpectationFailed, message, HttpStatusCode.ExpectationFailed);
        }

        /// <summary>
        /// Download PDF file
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="response"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected HttpResponseMessage Download(byte[] buffer, HttpResponseMessage response, string fileName)
        {
            try
            {
                if (buffer != null)
                {
                    MemoryStream memoryStream = new MemoryStream(buffer);
                    //content length for use in header
                    var contentLength = buffer.Length;

                    //successful
                    response.Content = new StreamContent(memoryStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentLength = contentLength;
                    ContentDispositionHeaderValue contentDisposition = null;
                    if (ContentDispositionHeaderValue.TryParse("attachment; filename=" + fileName + ".pdf", out contentDisposition))
                    {
                        response.Content.Headers.ContentDisposition = contentDisposition;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;
            }
            return response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ObjectCache cache = MemoryCache.Default;
                HttpClient httpClient = (HttpClient)cache["HttpClient"];
                if (httpClient != null) httpClient.Dispose();
            }
        }
    }
}