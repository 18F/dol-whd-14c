using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Moq;
using System;
using System.IO;
using DOL.WHD.Section14c.Business.Services;

namespace DOL.WHD.Section14c.Business.Helper.Tests
{
    [TestClass()]
    public class ApplicationDocumentHelperTests
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IApplicationService _applicationService;
        private readonly IFileRepository _fileRepositoryMock;
        private readonly IAttachmentRepository _attachmentRepositoryMock;

        public ApplicationDocumentHelperTests()
        {
            _fileRepositoryMock = new FileRepository(@"TestUploads\");
            _attachmentRepositoryMock = new AttachmentRepositoryMock();
            _applicationService = new ApplicationServiceMock();
            _attachmentService = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
        }
        [TestMethod()]
        public void ApplicationDataTest()
        {
            string testFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestFiles"));
            var applicationViewTemplatePath = Path.Combine(testFilePath, "Section14cApplicationPdfView.html");
            ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_applicationService, _attachmentService);
            var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F666") , applicationViewTemplatePath);
            Assert.AreEqual(6, applicationAttachmentsData.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ApplicationDataTest_Invalid()
        {
            string testFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestFiles"));
            var applicationViewTemplatePath = Path.Combine(testFilePath, "Section14cApplicationPdfView.html");
            ApplicationDocumentHelper applicationDocumentHelper = new ApplicationDocumentHelper(_applicationService, _attachmentService);
            var applicationAttachmentsData = applicationDocumentHelper.ApplicationData(new Guid("CE7F5AA5-6832-43FE-BAE1-80D14CD8F663"), applicationViewTemplatePath);
            Assert.AreEqual(6, applicationAttachmentsData.Count);
        }
    }
}