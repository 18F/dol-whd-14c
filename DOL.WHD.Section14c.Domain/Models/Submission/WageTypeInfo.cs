using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WageTypeInfo : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public int NumWorkers { get; set; }

        [Required]
        public string JobName { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public virtual Response PrevailingWageMethod { get; set; }

        public PrevailingWageSurveyInfo MostRecentPrevailingWageSurvey { get; set; }

        public AlternateWageData AlternateWageData { get; set; }

        public Attachment SCAWageDetermination { get; set; }

        // Documentation
        public Attachment Attachment { get; set; }
    }
}
