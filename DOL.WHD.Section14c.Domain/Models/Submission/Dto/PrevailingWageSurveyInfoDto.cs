using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class PrevailingWageSurveyInfoDto
    {
        [Required]
        public double PrevailingWageDetermined { get; set; }

        [Required]
        public IEnumerable<SourceEmployerDto> SourceEmployers { get; set; }

        // Prevailing Wage Determination - Hourly
        public Guid? AttachmentId { get; set; }
    }
}
