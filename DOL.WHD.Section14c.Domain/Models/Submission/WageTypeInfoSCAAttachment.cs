using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WageTypeInfoSCAAttachment
    {
        public string WageTypeInfoId { get; set; }
        public WageTypeInfo WageTypeInfo { get; set; }
        public string SCAAttachmentId { get; set; }
        public virtual Attachment SCAAttachment { get; set; }
        public string AttachmentName { get; set; }
    }
}
