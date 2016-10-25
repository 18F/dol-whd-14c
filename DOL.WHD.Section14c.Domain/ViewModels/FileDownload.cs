using System.IO;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class AttachementDownload
    {
        public Attachment Attachment { get; set; }
       
        public MemoryStream MemoryStream { get; set; }
    }
}
