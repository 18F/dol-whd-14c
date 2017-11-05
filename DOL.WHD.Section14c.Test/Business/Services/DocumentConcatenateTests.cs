using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.PdfApi.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System.IO;
using System.Reflection;
using System.Resources;
using DOL.WHD.Section14c.Test.Properties;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.PdfApi.Business.Tests
{
    [TestClass()]
    public class DocumentConcatenateTests
    {
        private readonly IDocumentConcatenate _documentConcatenate;
        private string testHtmlString;
        private string testPdfPath;
        private string testImagePath;
        private byte[] testPdfByteArray;
        private byte[] testImageByteArray;

        public DocumentConcatenateTests()
        {
            _documentConcatenate = new DocumentConcatenate();
        }

        [TestInitialize]
        public void Initialize()
        {
            testHtmlString = @"<html><body><h1>My Content</h1><p>My Content.</p><a href='#'></a></body></html>";
            testPdfPath = AppDomain.CurrentDomain.BaseDirectory + @"\TestFiles\TestFile1.pdf";
            testImagePath = AppDomain.CurrentDomain.BaseDirectory + (@"\TestFiles\TestImage.jpg");

            // The codeCoverage Tool can not access the test files. I am programmatically creating a Test PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            // Create an empty page
            PdfPage page = document.AddPage();
            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            testPdfByteArray = ms.ToArray();

            // The codeCoverage Tool can not access the test files. I am programmatically creating a testimage
            Bitmap bMap = new Bitmap(200, 100);
            Graphics flagGraphics = Graphics.FromImage(bMap);
            int red = 0;
            int white = 11;
            while (white <= 100)
            {
                flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
                flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
                red += 20;
                white += 20;
            }
            var memStream = new MemoryStream();
            bMap.Save(memStream, ImageFormat.Jpeg);
            testImageByteArray = memStream.ToArray();

            //var image = Resources.testImage;
            //using (var stream = new MemoryStream())
            //{
            //    image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //    testImageByteArray = stream.ToArray();
            //}
        }

        [TestMethod]
        public void ApplicationData_PublicProperties()
        {
            var testFileContents = "test";
            var filePath = new List<string>()
            {
                @"c:\temp\file1.pdf",
                @"c:\temp|file2.jpg"
            };
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var obj = new ApplicationData
            {
                Buffer = data,
                FilePaths = filePath,
                HtmlString= testHtmlString,
                Type= "pdf"
            };

            Assert.AreEqual(4, obj.Buffer.Length);
            Assert.AreEqual(@"c:\temp\file1.pdf", obj.FilePaths.FirstOrDefault());
            Assert.AreEqual(testHtmlString, obj.HtmlString);
            Assert.AreEqual("pdf", obj.Type);
        }

        [TestMethod()]
        public void ConcatenatePDf_CreateFromPdfByteTest()
        {
            List<ApplicationData> applicationData = new List<ApplicationData>();
            applicationData.Add(new ApplicationData() { Buffer = testPdfByteArray, Type="pdf"});
            var bytes = _documentConcatenate.Concatenate(applicationData);
            Assert.IsNotNull(bytes);
        }
        [TestMethod()]
        [ExpectedException(typeof(ApiException))]
        public void ConcatenatePDf_CreateFromPdfByteTest_Invalid()
        {
            List<ApplicationData> applicationData = new List<ApplicationData>();
            applicationData.Add(new ApplicationData() { Buffer = null, Type = "pdf" });
            var bytes = _documentConcatenate.Concatenate(applicationData);
        }

        [TestMethod()]
        public void ConcatenatePDf_CreateFromImageTest()
        {
            List<ApplicationData> applicationData = new List<ApplicationData>();
            applicationData.Add(new ApplicationData() { Buffer = testImageByteArray, Type = "image" });
            var bytes = _documentConcatenate.Concatenate(applicationData);
            Assert.IsNotNull(bytes);
        }

        [TestMethod()]
        [ExpectedException(typeof(ApiException))]
        public void ConcatenatePDf_CreateFromImageTest_Invalid()
        {
            List<ApplicationData> applicationData = new List<ApplicationData>();
            applicationData.Add(new ApplicationData() { Buffer = null, Type = "image" });
            var bytes = _documentConcatenate.Concatenate(applicationData);
        }

        [TestMethod()]
        public void ConcatenatePDf_CreateFromHtmlTest()
        {
            var data = Encoding.ASCII.GetBytes(testHtmlString);
            List<ApplicationData> applicationData = new List<ApplicationData>();
            applicationData.Add(new ApplicationData() { HtmlString = testHtmlString, Type = "html" });
            var bytes = _documentConcatenate.Concatenate(applicationData);
            Assert.IsNotNull(bytes);
        }

        [TestMethod()]
        public void ConcatenatePDf_CreateFromFilePathest()
        {
            var path = new List<string>()
            {
                testImagePath,
                testPdfPath
            };
            List<ApplicationData> applicationData = new List<ApplicationData>();
            applicationData.Add(new ApplicationData() { FilePaths = path, Type = "files" });
            var bytes = _documentConcatenate.Concatenate(applicationData);
            Assert.IsNotNull(bytes);
        }
    }
}