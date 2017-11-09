using DOL.WHD.Section14c.PdfApi.Business;
using HtmlAgilityPack;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TheArtOfDev.HtmlRenderer.PdfSharp;

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
        public static PdfDocument ConcatenatePDFs(PdfDocument outputDocument, PDFContentData applicationData)
        {
            if (!string.IsNullOrEmpty(applicationData.Type))
            {
                // Create PDF file
                if (applicationData.Type.ToLower().Contains("pdf"))
                {
                    outputDocument = ConcatenatePDFDocumentByBytes(outputDocument, applicationData.Buffer);
                }

                // Create PDF image
                if (applicationData.Type.ToLower().Contains("image"))
                {
                    var doc = ConcatenatePDFWithImage(outputDocument, applicationData.Buffer);
                    AddPagesToPdf(ref outputDocument, doc);
                }

                if (applicationData.Type.ToLower().Contains("html"))
                {
                    var doc = GetPdfDocFromHtml(outputDocument, applicationData.HtmlString);
                    AddPagesToPdf(ref outputDocument, doc);
                }
            }

            if (applicationData?.FilePaths != null)
            {
                outputDocument = ConcatenatePDFDocumentByPath(outputDocument, applicationData.FilePaths);
            }

            return outputDocument;
        }

        /// <summary>
        /// Create PDF from HTML string
        /// </summary>
        /// <param name="outputDocument"></param>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        private static PdfDocument GetPdfDocFromHtml(PdfDocument outputDocument, string htmlString)
        {
            if (!string.IsNullOrEmpty(htmlString))
            {
                htmlString = ScrubHtmlString(htmlString);
                
                outputDocument = PdfGenerator.GeneratePdf(htmlString, PageSize.A4);
            }

            return outputDocument;
        }

        /// <summary>
        /// Clean Html string and fix any html errors
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        private static string ScrubHtmlString(string htmlString)
        {
            string tempString = htmlString;
            if (!string.IsNullOrEmpty(htmlString))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlString);
                doc.OptionFixNestedTags = true;

                List<HtmlNode> nodes = doc.DocumentNode.Descendants().Where(x => x.Name == "a" && x.Attributes.Contains("href") && x.Attributes["href"].Value.StartsWith("#")).ToList();
               
                foreach (HtmlNode node in nodes)
                {
                    node.Attributes["href"].Value = "";
                }

                tempString = doc.DocumentNode.InnerHtml;
            }
            return tempString;
        }

        /// <summary>
        /// Concatenate PDF Document By Array
        /// </summary>
        /// <param name="documentContentByteArray"></param>
        /// <returns></returns>
        private static PdfDocument ConcatenatePDFDocumentByBytes(PdfDocument outputDocument, byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentException("No data provided", "buffer");

            using (var stream = new MemoryStream(buffer))
            {     
                // Open the document to import pages from it.
                var inputDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                   
                    // ...and add them to the output document.
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
        private static PdfDocument ConcatenatePDFWithImage(PdfDocument outputDocument, byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentException("No data provided", "buffer");

            using (var stream = new MemoryStream(buffer))
            {
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
        private static PdfDocument ConcatenatePDFDocumentByPath(PdfDocument outputDocument, List<string> filePaths)
        {
            var PdfConcatenateSupportedFileTypes = "pdf|png|jpg|jpeg";
            // Iterate files
            foreach (var file in filePaths)
            {
                if (string.IsNullOrEmpty(file))
                    throw new ArgumentException("No data provided", "file");
                
                var extension = Path.GetExtension(file);

                var regex = new Regex(PdfConcatenateSupportedFileTypes);

                bool containsAny = regex.IsMatch(extension.ToLower());

                if (containsAny)
                {
                    switch (extension.ToLower())
                    {
                        case ".pdf":
                            outputDocument = ConcatenatePDFByPath(outputDocument, file);
                            break;
                        default:
                            var pdfImageDoc = CreatePDFDocumentByImage(file);
                            // Handle PdfSharp Combine page error.
                            AddPagesToPdf(ref outputDocument, pdfImageDoc); 
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
            PdfDocument doc = new PdfDocument();
            // Open the document to import pages from it.
            var inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
            // Iterate pages
            int count = inputDocument.PageCount;
            for (int idx = 0; idx < count; idx++)
            {
                // Get the page from the external document...
                PdfPage page = inputDocument.Pages[idx];

                // ...and add them twice to the output document.
                outputDocument.AddPage(page);
            }
            return outputDocument;
        }

        /// <summary>
        /// Add Pdf page to combined pdf file
        /// </summary>
        /// <param name="mainDoc"></param>
        /// <param name="sourceDoc"></param>
        private static void AddPagesToPdf(ref PdfDocument mainDoc, PdfDocument sourceDoc)
        {
            MemoryStream tempMemoryStream = new MemoryStream();
            sourceDoc.Save(tempMemoryStream, false);

            PdfDocument openedDoc = PdfReader.Open(tempMemoryStream, PdfDocumentOpenMode.Import);
            foreach (PdfPage page in openedDoc.Pages)
            {
                mainDoc.AddPage(page);
            }
        }

        /// <summary>
        /// Create PDF from image file
        /// </summary>
        /// <param name="outputDocument"></param>
        /// <param name="imageLocation"></param>
        /// <returns></returns>
        private static PdfDocument CreatePDFDocumentByImage(string imageLocation)
        {
            PdfDocument doc = new PdfDocument();
            // Create an empty page
            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(doc.AddPage());

            DrawImage(gfx, imageLocation, 50, 50, 250, 250);
            
            return doc;
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