using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DOL.WHD.Section14c.PdfApi.PdfHelper
{
    public class PdfHelper
    {
        public static PdfDocument ConcatenatePDFDocuments(List<byte[]> documentContentByteArrays)
        {
            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            foreach (var bytes in documentContentByteArrays)
            {
                using (var memoryStream = new MemoryStream(bytes))
                {
                    // Open the document to import pages from it.
                    PdfDocument inputDocument = PdfReader.Open(memoryStream, PdfDocumentOpenMode.Import);

                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                }
            }
            return outputDocument;
        }

        public static PdfDocument ConcatenatePDFDocumentsByPath(List<string> filePaths)
        {
            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            foreach (var oath in filePaths)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(oath, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }
            return outputDocument;
        }
    }
}