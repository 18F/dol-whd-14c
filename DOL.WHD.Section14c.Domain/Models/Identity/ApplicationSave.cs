using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Attachment = DOL.WHD.Section14c.Domain.Models.Submission.Attachment;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public sealed class ApplicationSave : BaseEntity
    {
        public ApplicationSave()
        {
            if (Attachments == null)
                Attachments = new List<Attachment>();
        }

        [Key]
        public string EIN { get; set; }

        [Required]
        public string ApplicationState { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}
