using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Attachment = DOL.WHD.Section14c.Domain.Models.Submission.Attachment;
using DOL.WHD.Section14c.DataAccess;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ApplicationSave : BaseEntity
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

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
