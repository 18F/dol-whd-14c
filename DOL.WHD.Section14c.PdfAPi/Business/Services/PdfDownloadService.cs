using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DOL.WHD.Section14c.PdfApi.Business
{
    public class PdfDownloadService: IPdfDownloadService
    {
        /// <summary>
        /// Download PDF file
        /// </summary>
        /// <param name="pdfDocument"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponseMessage Download(PdfDocument pdfDocument, HttpRequestMessage request)
        {
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
            try
            {
                if (pdfDocument != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    pdfDocument.Save(memoryStream);
                    //get buffer
                    var buffer = memoryStream.ToArray();
                    //content length for use in header
                    var contentLength = buffer.Length;

                    //successful
                    response.Content = new StreamContent(memoryStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentLength = contentLength;
                    ContentDispositionHeaderValue contentDisposition = null;
                    if (ContentDispositionHeaderValue.TryParse("inline; filename=Concatenate.pdf", out contentDisposition))
                    {
                        response.Content.Headers.ContentDisposition = contentDisposition;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }
    }
}