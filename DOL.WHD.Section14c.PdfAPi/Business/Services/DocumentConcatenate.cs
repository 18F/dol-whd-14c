using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOL.WHD.Section14c.PdfApi.Business
{
    public class DocumentConcatenate: IDocumentConcatenate
    {
        public string Concatenate(List<byte[]> documentContentByteArrays)
        {
            string result = string.Empty;

            PdfDocument pdfDocument = PdfHelper.PdfHelper.ConcatenatePDFDocuments(documentContentByteArrays);
            
            return result;
        }

        public string Concatenate(List<string> filePaths)
        {
            string result = string.Empty;

            PdfDocument pdfDocument = PdfHelper.PdfHelper.ConcatenatePDFDocumentsByPath(filePaths);

            return result;
        }
    }
}