using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Moq;
using System;
using System.IO;
using DOL.WHD.Section14c.Business.Services;
using System.Collections.Generic;
using DOL.WHD.Section14c.PdfApi.PdfHelper;

namespace DOL.WHD.Section14c.Business.Helper.Tests
{
    [TestClass()]
    public class ApplicationDocumentHelperTests
    {
        private Mock<IApplicationService> _mockApplicationService;
        private Mock<IAttachmentService> _mockAttachmentService;

        public ApplicationDocumentHelperTests()
        {
            _mockApplicationService = new Mock<IApplicationService>();
            _mockAttachmentService = new Mock<IAttachmentService>();
        }

        [TestInitialize]
        public void Initialize()
        {
            _mockApplicationService.Reset();
            _mockAttachmentService.Reset();
        }

        [TestMethod()]
        public void ApplicationDataTest()
        {
            // Setup
            ApplicationSubmission testApplication = new ApplicationSubmission();
            List<Attachment> attachments = new List<Attachment>();
            List<PDFContentData> pdfContentList = new List<PDFContentData>();

            _mockApplicationService.Setup(mock => mock.GetApplicationById(It.IsAny<Guid>())).Returns(testApplication);

            _mockAttachmentService.Setup(mock => mock.GetApplicationFormViewContent(testApplication, It.IsAny<string>())).Returns("This is some HTML that is filled in");
            _mockAttachmentService.Setup(mock => mock.GetApplicationAttachments(testApplication)).Returns(attachments);
            _mockAttachmentService.Setup(mock => mock.PrepareApplicationContentsForPdfConcatenation(attachments, It.IsAny<string>())).Returns(pdfContentList);

            // Execute
            string testFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestFiles"));
            var applicationViewTemplatePath = Path.Combine(testFilePath, "Section14cApplicationPdfView.html");
            ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_mockApplicationService.Object, _mockAttachmentService.Object);
            var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F666") , applicationViewTemplatePath);

            // Assert
            _mockAttachmentService.Verify(mock => mock.PrepareApplicationContentsForPdfConcatenation(attachments, "This is some HTML that is filled in"), Times.Once());
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ApplicationDataTest_Invalid()
        {
            _mockApplicationService.Setup(mock => mock.GetApplicationById(It.IsAny<Guid>())).Returns((ApplicationSubmission)null);

            string testFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestFiles"));
            var applicationViewTemplatePath = Path.Combine(testFilePath, "Section14cApplicationPdfView.html");
            ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_mockApplicationService.Object, _mockAttachmentService.Object);
            var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F663"), applicationViewTemplatePath);
        }
    }
}
