using System;
using System.Collections.Generic;
using System.Linq;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WageTypeInfo : BaseEntity
    {
        public WageTypeInfo()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public int? NumWorkers { get; set; }

        public string JobName { get; set; }

        public string JobDescription { get; set; }

        public int? PrevailingWageMethodId { get; set; }
        public virtual Response PrevailingWageMethod { get; set; }

        public virtual PrevailingWageSurveyInfo MostRecentPrevailingWageSurvey { get; set; }

        public virtual AlternateWageData AlternateWageData { get; set; }

        public IEnumerable<string> SCAAttachmentIds
        {
            set
            {
                if (value != null)
                {
                    SCAAttachments = value.Select(
                        x =>
                            new WageTypeInfoSCAAttachment
                            {
                                SCAAttachmentId = x,
                                WageTypeInfoId = Id,
                            }).ToList();
                }
            }
        }
        public virtual ICollection<WageTypeInfoSCAAttachment> SCAAttachments { get; set; }

        // Documentation
        public string AttachmentId { get; set; }
        public virtual Attachment Attachment { get; set; }
    }
}
