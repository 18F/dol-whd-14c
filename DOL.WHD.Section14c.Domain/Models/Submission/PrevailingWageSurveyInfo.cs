using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class PrevailingWageSurveyInfo : BaseEntity
    {
        public PrevailingWageSurveyInfo()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public double PrevailingWageDetermined { get; set; }

        public virtual ICollection<SourceEmployer> SourceEmployers { get; set; }

        // Prevailing Wage Determination - Hourly
        public Guid? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
