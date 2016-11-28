using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WageTypeInfo : BaseEntity
    {
        public WageTypeInfo()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int? NumWorkers { get; set; }

        public string JobName { get; set; }

        public string JobDescription { get; set; }

        public int? PrevailingWageMethodId { get; set; }
        public virtual Response PrevailingWageMethod { get; set; }

        public virtual PrevailingWageSurveyInfo MostRecentPrevailingWageSurvey { get; set; }

        public virtual AlternateWageData AlternateWageData { get; set; }

        public Guid? SCAWageDeterminationAttachmentId { get; set; }
        public virtual Attachment SCAWageDeterminationAttachment { get; set; }

        // Documentation
        public Guid? AttachmentId { get; set; }
        public virtual Attachment Attachment { get; set; }
    }
}
