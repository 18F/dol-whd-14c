using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PdfSharp.Pdf;

namespace DOL.WHD.Section14c.PdfApi.PdfHelper.Tests
{
    [TestClass()]
    public class PdfHelperTests
    {
        private string testHtmlString;
        private string testPdfPath;
        private string testImagePath;
        private byte[] testPdfByteArray;
        private byte[] testImageByteArray;
        private PdfDocument outputDocument;

        [TestInitialize]
        public void Initialize()
        {
            testHtmlString = @"<html><body><h1>My Content</h1><p>My Content.</p><a href='#'></a></body></html>";
            string testFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestFiles"));
            testPdfPath = Path.Combine(testFilePath, "TestFile1.pdf");
            testImagePath = Path.Combine(testFilePath, "TestImage.jpg");

            testPdfByteArray = File.ReadAllBytes(testPdfPath);
            testImageByteArray = File.ReadAllBytes(testImagePath);
            outputDocument = new PdfDocument();
        }

        [TestMethod()]
        public void TestPdfByteArrayToPDF()
        {
            PDFContentData contentData = new PDFContentData {
                Buffer = testPdfByteArray,
                Type = "pdf",
            };            
            PdfDocument doc = PdfHelper.ConcatenatePDFs(outputDocument, contentData);
            Assert.IsNotNull(doc);
        }

        [TestMethod()]
        public void TestImageByteArrayToPdf()
        {
            PDFContentData contentData = new PDFContentData
            {
                Buffer = testImageByteArray,
                Type = "image",
            };
            PdfDocument doc = PdfHelper.ConcatenatePDFs(outputDocument, contentData);
            Assert.IsNotNull(doc);
        }

        [TestMethod()]
        public void TestHtmlToPdf()
        {
            PDFContentData contentData = new PDFContentData
            {
                HtmlString = testHtmlString,
                Type = "html",
            };
            PdfDocument doc = PdfHelper.ConcatenatePDFs(outputDocument, contentData);
            Assert.IsNotNull(doc);
        }
    }
}