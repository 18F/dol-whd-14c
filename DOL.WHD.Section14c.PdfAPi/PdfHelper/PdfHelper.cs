using DOL.WHD.Section14c.PdfApi.Business;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DOL.WHD.Section14c.PdfApi.PdfHelper
{
    public class PdfHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputDocument"></param>
        /// <param name="applicationData"></param>
        /// <returns></returns>
        public static PdfDocument ConcatenatePDFDocumentByByteArray(PdfDocument outputDocument, ApplicationData applicationData)
        {
            // Create PDF file
            if (applicationData.Type.ToLower().Contains("pdf"))
                outputDocument = ConcatenatePDFDocumenByArray(outputDocument, applicationData.Buffer);

            // Create PDF image
            if (applicationData.Type.ToLower().Contains("image"))
                outputDocument = ConcatenatePDFWithImage(outputDocument, applicationData.Buffer);

            return outputDocument;
        }
        /// <summary>
        /// Concatenate PDF Document By Array
        /// </summary>
        /// <param name="documentContentByteArray"></param>
        /// <returns></returns>
        public static PdfDocument ConcatenatePDFDocumenByArray(PdfDocument outputDocument, byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                if (stream == null)
                    throw new ArgumentException("No resource");

                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

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

        /// <summary>
        /// Create PDF from Image
        /// </summary>
        /// <param name="outputDocument"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static PdfDocument ConcatenatePDFWithImage(PdfDocument outputDocument, byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                if (stream == null)
                    throw new ArgumentException("No resource");
                
                // Create an empty page
                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(outputDocument.AddPage());

                Image image = Image.FromStream(stream);

                XImage img = XImage.FromGdiPlusImage(image);
                gfx.DrawImage(img, 0, 0);
            }
            return outputDocument;
        }        

        /// <summary>
        /// Concatenate PDF Document By File Path
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public static PdfDocument ConcatenatePDFDocumentByPath(PdfDocument outputDocument, List<string> filePaths)
        {
            // Iterate files
            foreach (var file in filePaths)
            {
                var extension = Path.GetExtension(file);

                Regex r = new Regex(Constants.PdfConcatenateSupportedFileTypes);

                bool containsAny = r.IsMatch(extension.ToLower());

                if (containsAny)
                {
                    switch (extension.ToLower())
                    {
                        case "pdf":
                            outputDocument = ConcatenatePDFByPath(outputDocument, file);
                            break;
                        default:
                            outputDocument = ConcatenatePDFByImageLocation(outputDocument, file);
                            break;
                    }
                }
            }
            return outputDocument;
        }

        /// <summary>
        /// Create PDF from existing PDF files
        /// </summary>
        /// <param name="outputDocument"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private static PdfDocument ConcatenatePDFByPath(PdfDocument outputDocument, string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

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

        /// <summary>
        /// Create PDF from image file
        /// </summary>
        /// <param name="outputDocument"></param>
        /// <param name="imageLocation"></param>
        /// <returns></returns>
        private static PdfDocument ConcatenatePDFByImageLocation(PdfDocument outputDocument, string imageLocation)
        {
            if (!string.IsNullOrEmpty(imageLocation))
            {
                // Create an empty page
                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(outputDocument.AddPage());

                DrawImage(gfx, imageLocation, 50, 50, 250, 250);
            }
            return outputDocument;
        }

        /// <summary>
        ///  create image from file
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="imagePath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawImage(XGraphics gfx, string imagePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(imagePath);
            gfx.DrawImage(image, x, y, width, height);
        }
    }
}