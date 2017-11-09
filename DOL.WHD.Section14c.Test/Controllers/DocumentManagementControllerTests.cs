using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.PdfApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.PdfApi.Business;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using Moq;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.PdfApi.Controllers.Tests
{
    [TestClass()]
    public class DocumentManagementControllerTests
    {
        private IDocumentConcatenate _documentConcatenateService;

        [TestInitialize]
        public void Initialize()
        {
            _documentConcatenateService = new DocumentConcatenate();
        }

        [TestMethod()]
        public void ConcatenateTest()
        {
            var testHtmlString = @"<html><body> <h1>My Content</h1><p>My Content.</p></body></html>";
            List<PDFContentData> applicationData = new List<PDFContentData>();
            applicationData.Add(new PDFContentData() { HtmlString = testHtmlString, Type = "html" });
            DocumentManagementController documentManagementController = new DocumentManagementController(_documentConcatenateService);
            var bytes = documentManagementController.Concatenate(applicationData);
            Assert.IsNotNull(bytes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConcatenateTest_Invalid()
        {
            List<PDFContentData> applicationData = new List<PDFContentData>();
            DocumentManagementController documentManagementController = new DocumentManagementController(_documentConcatenateService);
            documentManagementController.Concatenate(null);            
        }

        [TestMethod]
        public void ConcatenateTest_CORS()
        {
            List<PDFContentData> applicationData = new List<PDFContentData>();
            DocumentManagementController documentManagementController = new DocumentManagementController(_documentConcatenateService);
            var response = documentManagementController.Options();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}