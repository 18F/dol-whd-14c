using System;
using System.ComponentModel.DataAnnotations;
﻿using System.Collections.Generic;
 using Attachment = DOL.WHD.Section14c.Domain.Models.Submission.Attachment;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ApplicationSave
    {
        public ApplicationSave()
        {
            ApplicationId = Guid.NewGuid();

            if (Attachments == null)
                Attachments = new List<Attachment>();
        }

        [Key]
        public string EIN { get; set; }

        public Guid ApplicationId { get; set; }

        [Required]
        public string ApplicationState { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
