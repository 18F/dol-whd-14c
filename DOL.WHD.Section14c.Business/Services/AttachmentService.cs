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

        public AttachmentService(IFileRepository fileRepository, IAttachmentRepository attachmentRepository)
        {
            _fileRepository = fileRepository;
            _attachmentRepository = attachmentRepository;
        }

        public Attachment UploadAttachment(string EIN, MemoryStream memoryStream, string fileName, string fileType)
        {
            var fileUpload = new Attachment()
            {
                FileSize = memoryStream.Length,
                MimeType = fileType,
                OriginalFileName = fileName,
                Deleted = false,
                EIN = EIN
            };

            fileUpload.RepositoryFilePath = $@"{EIN}\{fileUpload.Id}";

            _fileRepository.Upload(memoryStream, fileUpload.RepositoryFilePath);

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
                if(application?.Employer?.SCAAttachment != null)
                    attachments.Add(application.Employer.SCAAttachment);

                if (application?.PieceRateWageInfo?.SCAWageDeterminationAttachment != null)
                    attachments.Add(application.PieceRateWageInfo.SCAWageDeterminationAttachment);

                if(application?.PieceRateWageInfo?.Attachment != null)
                    attachments.Add(application.PieceRateWageInfo.Attachment);

                if (application?.HourlyWageInfo?.MostRecentPrevailingWageSurvey?.Attachment != null)
                    attachments.Add(application.HourlyWageInfo.MostRecentPrevailingWageSurvey.Attachment);

                if (application?.HourlyWageInfo?.Attachment != null)
                    attachments.Add(application.HourlyWageInfo.Attachment);  
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

        public string GetApplicationFormViewContent(ApplicationSubmission application, string templateString)
        {
            string tempString = string.Empty;
            tempString = ApplicationFormViewHelper.PopulateHtmlTemplateWithApplicationData(application, templateString);
            return tempString;
        }

        public void Dispose()
        {
            _attachmentRepository.Dispose();
        }
    }
}
