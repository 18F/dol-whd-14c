using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class WageTypeInfoDto
    {
        [Required]
        public int NumWorkers { get; set; }

        [Required]
        public string JobName { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public int PrevailingWageMethodId { get; set; }

        public PrevailingWageSurveyInfoDto MostRecentPrevailingWageSurvey { get; set; }

        public AlternateWageDataDto AlternateWageData { get; set; }

        public Guid? SCAWageDeterminationId { get; set; }

        // Documentation
        public Guid? AttachmentId { get; set; }
    }
}
