using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class EmployerInfoSCAAttachment
    {
        public string EmployerInfoId { get; set; }
        public EmployerInfo EmployerInfo { get; set; }
        public string SCAAttachmentId { get; set; }
        public virtual Attachment SCAAttachment { get; set; }
        public string AttachmentName { get; set; }
    }
}
