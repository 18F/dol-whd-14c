using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace DOL.WHD.Section14c.Business.Helper
{
    public class ApplicationDocumentHelper
    {
        private readonly IApplicationService _applicationService;
        private readonly IAttachmentService _attachmentService;
        /// <summary>
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="applicationService"></param>
        /// <param name="saveService"></param>
        public ApplicationDocumentHelper(IApplicationService applicationService, IAttachmentService attachmentService)
        {
            _applicationService = applicationService;
            _attachmentService = attachmentService;
        }

        public List<ApplicationData> ApplicationData(Guid applicationId, string applicationViewTemplatePath)
        {
            // Get Application Template
            var applicationViewTemplateString = File.ReadAllText(applicationViewTemplatePath);

            var application = _applicationService.GetApplicationById(applicationId);

            if (application == null)
                throw new Exception("Application not found");

            var applicationHtml = _attachmentService.ApplicationFormView(application, applicationViewTemplateString);

            // Get all attachments from current application
            var getApplicationAttachments = _attachmentService.GetApplicationAttachments(application);

            // Prepare attachemnt for PDF generation
            var applicationAttachmentsData = _attachmentService.DownloadApplicationAttachments(
                                            getApplicationAttachments,
                                            applicationHtml);


            return applicationAttachmentsData;
        }
    }
}
