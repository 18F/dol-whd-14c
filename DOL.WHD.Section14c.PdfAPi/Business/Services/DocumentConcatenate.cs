using DOL.WHD.Section14c.Log.LogHelper;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
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
    public class DocumentConcatenate: IDocumentConcatenate
    {
        /// <summary>
        /// Create concatenate Pdf file By File Byte Array
        /// </summary>
        /// <param name="ApplicationDataCollection"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public byte[] Concatenate(List<PDFContentData>applicationDataCollection)
        {
            try
            {
                // Open the output document
                PdfDocument outputDocument = new PdfDocument();
                foreach (var applicationData in applicationDataCollection)
                {
                    if (applicationData != null)
                    {
                        outputDocument = PdfHelper.PdfHelper.ConcatenatePDFs(outputDocument, applicationData);
                    }
                }
                // Conver to Byte array
                MemoryStream memoryStream = new MemoryStream();
                outputDocument.Save(memoryStream);
                //get buffer
                var buffer = memoryStream.ToArray();
                return buffer;
            }
            catch(Exception ex)
            {
                throw new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, HttpStatusCode.InternalServerError);
            }   
        }        
    }
}