using System;
using System.Data;
using System.IO;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.ViewModels;
using System.Collections.Generic;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using DOL.WHD.Section14c.Business.Helper;

namespace DOL.WHD.Section14c.Business.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private bool Disposed = false;

        public AttachmentService(IFileRepository fileRepository, IAttachmentRepository attachmentRepository)
        {
            _fileRepository = fileRepository;
            _attachmentRepository = attachmentRepository;
        }

        public Attachment UploadAttachment(string EIN, byte[] bytes, string fileName, string fileType)
        {
            var fileUpload = new Attachment()
            {
                FileSize = bytes.Length,
                MimeType = fileType,
                OriginalFileName = fileName,
                Deleted = false,
                ApplicationId = EIN
            };

            fileUpload.RepositoryFilePath = $@"{EIN}\{fileUpload.Id}";

            _fileRepository.Upload(bytes, fileUpload.RepositoryFilePath);

            _attachmentRepository.Add(fileUpload);
            _attachmentRepository.SaveChanges();

            return fileUpload;
        }

        public AttachementDownload DownloadAttachment(MemoryStream memoryStream, string EIN, Guid fileId)
        {
            var attachment = _attachmentRepository.Get()
                .Where(x => x.ApplicationId == EIN)
                .SingleOrDefault(x => x.Deleted == false && x.Id == fileId.ToString());

            if (attachment == null)
                throw new ObjectNotFoundException();

            var stream = _fileRepository.Download(memoryStream, attachment.RepositoryFilePath);

            return new AttachementDownload()
            {
                MemoryStream = stream,
                Attachment = attachment
            };
        }

        /// <summary>
        ///     Builds a list of PDF content data objects from the list
        ///     of attachments and a given HTML form
        /// </summary>
        /// <param name="attachments">
        ///     The list of attachments that should be in the document
        /// </param>
        /// <param name="applicationFormData">
        ///     The HTML form that should be in the document
        /// </param>
        /// <returns>
        ///     A list of PDF content data objects that can be sent to
        ///     the PDF API to generate a new PDF document
        /// </returns>
        public List<PDFContentData> PrepareApplicationContentsForPdfConcatenation(Dictionary<string, Attachment> attachments, List<string> applicationFormData)
        {
            var applicationData = new List<PDFContentData>();

            if (applicationFormData != null)
            {
                applicationData.Add(new PDFContentData() { HtmlString = applicationFormData, Type = "html" });
            }
            foreach (var attachment in attachments)
            {
                if (attachment.Value != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // Create file name for each attachment. 
                        // File name format: attachment type - original file name 
                        var fileName = string.Format("{0} - {1}", attachment.Key,  attachment.Value.OriginalFileName);
                        var stream = _fileRepository.Download(memoryStream, attachment.Value.RepositoryFilePath);
                        applicationData.Add(new PDFContentData() { Buffer = stream.ToArray(), Type = attachment.Value.MimeType, FileName = fileName });
                    }
                }
            }
            return applicationData;
        }

        /// <summary>
        /// Get all attachments from an application after the application has been submitted.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Dictionary<string, Attachment> GetApplicationAttachments(ref ApplicationSubmission application)
        {
            var attachments = new Dictionary<string, Attachment>();
            var applicationSubmission = application;
            if (application != null)
            {
                var count = 1;
                if (application.Employer?.SCAAttachments != null)
                {
                    foreach (var item in application.Employer.SCAAttachments)
                    {
                        var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == item.SCAAttachmentId);
                        attachments.Add(string.Format("SCA Wage Determination Attachment {0}", (count++) ), attachment);
                    }
                }

                if (application.PieceRateWageInfo?.SCAAttachments != null)
                {
                    count = 1;
                    foreach (var item in application.PieceRateWageInfo.SCAAttachments)
                    {
                        var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == item.SCAAttachmentId);
                        attachments.Add(string.Format("Piece Rate Wage Info ScaWage Determination Attachment  {0}", (count++)), attachment);
                    }
                }

                if (application.PieceRateWageInfo?.AttachmentId != null)
                {
                    attachments.Add("Piece Rate Wage Info Attachment", application.PieceRateWageInfo.Attachment);
                }

                if (application.HourlyWageInfo?.SCAAttachments != null)
                {
                    count = 1;
                    foreach (var item in application.HourlyWageInfo.SCAAttachments)
                    {
                        var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == item.SCAAttachmentId);
                        attachments.Add(string.Format("Hourly Wage Info ScaWage Determination Attachment  {0}", (count++)), attachment);
                    }
                }

                if (application.HourlyWageInfo?.MostRecentPrevailingWageSurvey?.AttachmentId != null)
                {
                    attachments.Add("Hourly Wage Info SCA Wage Determination Attachment", application.HourlyWageInfo.MostRecentPrevailingWageSurvey.Attachment);
                }

                if (application.HourlyWageInfo?.AttachmentId != null)
                {
                    attachments.Add("Hourly Wage Info Attachmen", application.HourlyWageInfo.Attachment);
                }
            }
            return attachments;
        }

        public void DeleteAttachement(string EIN, Guid fileId)
        {
            var attachment = _attachmentRepository.Get()
                .Where(x => x.ApplicationId == EIN)
                .SingleOrDefault(x => x.Deleted == false && x.Id == fileId.ToString());

            if (attachment == null)
                throw new ObjectNotFoundException();

            attachment.Deleted = true;

            _attachmentRepository.SaveChanges();
        }

        public void DeleteApplicationAttachements(string applicationId)
        {
            var attachments = _attachmentRepository.Get()
                .Where(x => x.ApplicationId == applicationId && x.Deleted == false);
            if(attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    attachment.Deleted = true;
                }
                _attachmentRepository.SaveChanges();
            }
        }

        /// <summary>
        ///     Given a 14c application and an HTML template string,
        ///     build a populated HTML string
        /// </summary>
        /// <param name="application">
        ///     The 14c application object
        /// </param>
        /// <param name="templateString">
        ///     The HTML template string to populate
        /// </param>
        /// <returns>
        ///     A popualted HTML string
        /// </returns>
        public string GetApplicationFormViewContent(ApplicationSubmission application, string templateString)
        {
            string tempString = string.Empty;
            tempString = ApplicationFormViewHelper.PopulateHtmlTemplateWithApplicationData(application, templateString);
            return tempString;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _attachmentRepository.Dispose();
            }
        }
    }
}
