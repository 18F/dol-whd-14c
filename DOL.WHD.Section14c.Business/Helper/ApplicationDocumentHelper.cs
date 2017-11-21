using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly IResponseService _responseService;
        /// <summary>
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="applicationService"></param>
        /// <param name="saveService"></param>
        public ApplicationDocumentHelper(IApplicationService applicationService, IAttachmentService attachmentService, IResponseService responseService)
        {
            _applicationService = applicationService;
            _attachmentService = attachmentService;
            _responseService = responseService;
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
            LoadApplicationData(ref application);

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

        /// <summary>
        /// Set application submission related object for pdf generation
        /// </summary>
        /// <param name="application">
        /// Application Submission
        /// </param>
        private void LoadApplicationData(ref ApplicationSubmission application)
        {
            // Load WIOAWorker related data
            if (application.WIOA != null && application.WIOA.WIOAWorkers != null)
            {                
                var workers = new List<WIOAWorker>();
                foreach (var worker in application.WIOA.WIOAWorkers)
                {
                    worker.WIOAWorkerVerified = _responseService.GetResponseById(worker.WIOAWorkerVerifiedId.ToString());
                    workers.Add(worker);
                };
                application.WIOA.WIOAWorkers = workers;
            }
            // Load work site related data
            if (application.WorkSites != null)
            {                
                var workSites = new List<WorkSite>();
                foreach (var wSite in application.WorkSites)
                {
                    wSite.WorkSiteType = _responseService.GetResponseById(wSite.WorkSiteTypeId.ToString());                    
                    workSites.Add(wSite);
                };
                application.WorkSites = workSites;
            }
            // Load application establishment types related data
            if (application.EstablishmentType != null)
            {
                var applicationSubmissionEstablishmentTypes = new List<ApplicationSubmissionEstablishmentType>();
                foreach(var type in application.EstablishmentType)
                {
                    type.EstablishmentType = _responseService.GetResponseById(type.EstablishmentTypeId.ToString());
                    applicationSubmissionEstablishmentTypes.Add(type);
                }
                application.EstablishmentType = applicationSubmissionEstablishmentTypes;
            }
        }        
    }
}
