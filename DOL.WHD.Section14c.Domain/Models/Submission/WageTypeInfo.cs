using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WageTypeInfo
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

        // TODO: required if PrevailingWageMethod == Prevailing Wage Survey
        public PrevailingWageSurveyInfo MostRecentPrevailingWageSurvey { get; set; }
    }
}
