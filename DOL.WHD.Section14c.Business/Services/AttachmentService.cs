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
                EIN = EIN
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
                .Where(x => x.EIN == EIN)
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
        public List<PDFContentData> PrepareApplicationContentsForPdfConcatenation(List<Attachment> attachments, string applicationFormData)
        {
            var applicationData = new List<PDFContentData>();

            if (!string.IsNullOrEmpty(applicationFormData))
            {
                applicationData.Add(new PDFContentData() { HtmlString = applicationFormData, Type = "html" });
            }
            foreach (var attachment in attachments)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var stream = _fileRepository.Download(memoryStream, attachment.RepositoryFilePath);
                    applicationData.Add(new PDFContentData() { Buffer = stream.ToArray(), Type = attachment.MimeType });
                }
            }
            return applicationData;
        }

        /// <summary>
        /// Get all attachments from an application after the application has been submitted.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public List<Attachment> GetApplicationAttachments(ApplicationSubmission application)
        {
            List<Attachment> attachments = new List<Attachment>();
            if (application != null)
            {
                if (application.Employer?.SCAAttachmentId != null)
                {
                    // The attachment is null right after application is submitted. 
                    // In order to be able to send application submit email to employer with concatenate pdf document 
                    // The attachment need to get from attachment database table directly
                    var attachment = _attachmentRepository.Get()
                        .SingleOrDefault(x => x.Deleted == false && x.Id == application.Employer?.SCAAttachmentId.ToString());
                    attachments.Add(attachment);
                }

                if (application.PieceRateWageInfo?.SCAWageDeterminationAttachmentId != null)
                {
                    var attachment = _attachmentRepository.Get()
                        .SingleOrDefault(x => x.Deleted == false && x.Id == application.PieceRateWageInfo?.SCAWageDeterminationAttachmentId.ToString());
                    attachments.Add(application.PieceRateWageInfo.SCAWageDeterminationAttachment);
                }

                if (application.PieceRateWageInfo?.AttachmentId != null)
                {
                    var attachment = _attachmentRepository.Get()
                        .SingleOrDefault(x => x.Deleted == false && x.Id == application.PieceRateWageInfo?.AttachmentId.ToString());
                    attachments.Add(application.PieceRateWageInfo.Attachment);
                }

                if (application.HourlyWageInfo?.MostRecentPrevailingWageSurvey?.AttachmentId != null)
                {
                    var attachment = _attachmentRepository.Get()
                        .SingleOrDefault(x => x.Deleted == false && x.Id == application.HourlyWageInfo?.MostRecentPrevailingWageSurvey?.AttachmentId.ToString());
                    attachments.Add(application.HourlyWageInfo.MostRecentPrevailingWageSurvey.Attachment);
                }

                if (application.HourlyWageInfo?.AttachmentId != null)
                {
                    var attachment = _attachmentRepository.Get()
                       .SingleOrDefault(x => x.Deleted == false && x.Id == application.HourlyWageInfo?.AttachmentId.ToString());
                    attachments.Add(application.HourlyWageInfo.Attachment);
                }
            }
            return attachments;
        }

        public void DeleteAttachement(string EIN, Guid fileId)
        {
            var attachment = _attachmentRepository.Get()
                .Where(x => x.EIN == EIN)
                .SingleOrDefault(x => x.Deleted == false && x.Id == fileId.ToString());

            if (attachment == null)
                throw new ObjectNotFoundException();

            attachment.Deleted = true;

            _attachmentRepository.SaveChanges();
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
