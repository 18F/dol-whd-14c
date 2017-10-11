using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class PrevailingWageSurveyInfo : BaseEntity
    {
        public PrevailingWageSurveyInfo()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public double? PrevailingWageDetermined { get; set; }

        public virtual ICollection<SourceEmployer> SourceEmployers { get; set; }

        // Prevailing Wage Determination - Hourly
        public string AttachmentId { get; set; }
        public virtual Attachment Attachment { get; set; }
    }
}
