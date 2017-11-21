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
using System.Diagnostics;

namespace DOL.WHD.Section14c.Business.Helper.Tests
{
    [TestClass()]
    public class ApplicationDocumentHelperTests
    {
        private Mock<IApplicationService> _mockApplicationService;
        private Mock<IAttachmentService> _mockAttachmentService;
        private Mock<IResponseService> _mockResponseService;
        private string testFilePath;
        public ApplicationDocumentHelperTests()
        {
            _mockApplicationService = new Mock<IApplicationService>();
            _mockAttachmentService = new Mock<IAttachmentService>();
            _mockResponseService = new Mock<IResponseService>();
        }

        [TestInitialize]
        public void Initialize()
        {
            testFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles"));
            if (!Directory.Exists(testFilePath))
                testFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestFiles"));
            EventLog.WriteEntry("Test", testFilePath);
            EventLog.WriteEntry("Test", AppDomain.CurrentDomain.BaseDirectory);
            _mockApplicationService.Reset();
            _mockAttachmentService.Reset();
            _mockResponseService.Reset();
        }

        [TestMethod()]
        public void ApplicationDataTest()
        {
            // Setup
            ApplicationSubmission testApplication = new ApplicationSubmission();
            var wioaworker = new List<WIOAWorker>() { new WIOAWorker() { WIOAWorkerVerifiedId = 1 } };
            var wIOA = new WIOA() { WIOAWorkers = wioaworker };
            testApplication.WIOA = wIOA;
            var workSites = new List<WorkSite>() { new WorkSite(){ WorkSiteTypeId=1 }};
            testApplication.WorkSites = workSites;            
            var applicationSubmissionEstablishmentTypes = new List<ApplicationSubmissionEstablishmentType>() { new ApplicationSubmissionEstablishmentType() { EstablishmentTypeId = 1 } };
            testApplication.EstablishmentType = applicationSubmissionEstablishmentTypes;

            Dictionary<string, Attachment> attachments = new Dictionary<string, Attachment>();
            List<PDFContentData> pdfContentList = new List<PDFContentData>();
            Response response = new Response();

            _mockApplicationService.Setup(mock => mock.GetApplicationById(It.IsAny<Guid>())).Returns(testApplication);

            _mockAttachmentService.Setup(mock => mock.GetApplicationFormViewContent(testApplication, It.IsAny<string>())).Returns("This is some HTML that is filled in");
            _mockAttachmentService.Setup(mock => mock.GetApplicationAttachments(ref testApplication)).Returns(attachments);
            _mockAttachmentService.Setup(mock => mock.PrepareApplicationContentsForPdfConcatenation(attachments, It.IsAny<List<string>>())).Returns(pdfContentList);

            _mockResponseService.Setup(mock => mock.GetResponseById(It.IsAny<string>())).Returns(response);
            // Execute            
            var applicationViewTemplatePath = Path.Combine(testFilePath, "Section14cApplicationPdfView.html");
            ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_mockApplicationService.Object, _mockAttachmentService.Object, _mockResponseService.Object);
            var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F666"), new List<string>() { applicationViewTemplatePath });

            // Assert
            _mockAttachmentService.Verify(mock => mock.PrepareApplicationContentsForPdfConcatenation(attachments, new List<string>() { "This is some HTML that is filled in" }), Times.Once());
            Assert.AreEqual(pdfContentList, applicationAttachmentsData, "returns the expected data");
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ApplicationDataTest_Invalid()
        {
            _mockApplicationService.Setup(mock => mock.GetApplicationById(It.IsAny<Guid>())).Returns((ApplicationSubmission)null);
            
            var applicationViewTemplatePath = Path.Combine(testFilePath, "Section14cApplicationPdfView.html");
            ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_mockApplicationService.Object, _mockAttachmentService.Object, _mockResponseService.Object);
            var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F663"), new List<string>() { applicationViewTemplatePath });
        }
    }
}
