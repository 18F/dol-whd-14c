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
        /// <param name="documentContentByteArrays"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public PdfDocument Concatenate(List<ApplicationData> documentContentByteArrays)
        {
            // Open the output document
            PdfDocument outputDocument = new PdfDocument();
            foreach (var applicationData in documentContentByteArrays)
            {
                if (applicationData.FilePaths != null)
                {
                    outputDocument = PdfHelper.PdfHelper.ConcatenatePDFDocumentByPath(outputDocument, applicationData.FilePaths);
                }
                else
                {
                    outputDocument = PdfHelper.PdfHelper.ConcatenatePDFDocumentByByteArray(outputDocument, applicationData);
                }
            }
            return outputDocument;
        }        
    }
}