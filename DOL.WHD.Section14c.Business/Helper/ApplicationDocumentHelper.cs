using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace DOL.WHD.Section14c.Business.Helper
{
    /// <summary>
    /// Application Document Helper
    /// </summary>
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

        /// <summary>
        /// Get Application Data
        /// </summary>
        /// <param name="applicationId">
        /// Application GUID
        /// </param>
        /// <param name="applicationTemplatesPath">
        /// Complete file path for each html template
        /// </param>
        /// <returns></returns>
        public List<PDFContentData> ApplicationData(Guid applicationId, List<string> applicationTemplatesPath)
        {
            var application = _applicationService.GetApplicationById(applicationId);

            if (application == null)
                throw new Exception("Application not found");

            // Get all attachments from current application
            var getApplicationAttachments = _attachmentService.GetApplicationAttachments(ref application);

            var htmlTemplates = new List<string>();
            foreach (string path in applicationTemplatesPath)
            {
                // Get Application Template
                var templatString = File.ReadAllText(path);
                var htmlString = _attachmentService.GetApplicationFormViewContent(application, templatString);
                htmlTemplates.Add(htmlString);
            }

            // Prepare attachemnt for PDF generation
            var applicationAttachmentsData = _attachmentService.PrepareApplicationContentsForPdfConcatenation(
                                            getApplicationAttachments,
                                            htmlTemplates);

            return applicationAttachmentsData;
        }
    }
}
