using System;
using System.IO;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.ViewModels;
using System.Collections.Generic;
using DOL.WHD.Section14c.PdfApi.Business;
using DOL.WHD.Section14c.PdfApi.PdfHelper;

namespace DOL.WHD.Section14c.Business
{
    public interface IAttachmentService : IDisposable
    {
        Attachment UploadAttachment(string EIN, byte[] bytes, string fileName, string fileType);
        AttachementDownload DownloadAttachment(MemoryStream memoryStream, string EIN, Guid fileId);
        List<Attachment> GetApplicationAttachments(ref ApplicationSubmission application);
        List<PDFContentData> PrepareApplicationContentsForPdfConcatenation(List<Attachment> attachments, List<string> applicationFormData);
        void DeleteAttachement(string EIN, Guid fileId);
        string GetApplicationFormViewContent(ApplicationSubmission application, string templateString);
    }
}