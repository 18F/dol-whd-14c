using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Response PrimaryDisability { get; set; }

        // TODO: required if PrimaryDisability == Other
        public string PrimaryDisabilityOther { get; set; }

        [Required]
        public string WorkType { get; set; }

        [Required]
        public int NumJobs { get; set; }

        [Required]
        public double AvgWeeklyHours { get; set; }

        [Required]
        public double AvgHourlyEarnings { get; set; }

        [Required]
        public double PrevailingWage { get; set; }

        [Required]
        public double ProductivityMeasure { get; set; }

        [Required]
        public string CommensurateWageRate { get; set; }

        [Required]
        public double TotalHours { get; set; }

        [Required]
        public bool WorkAtOtherSite { get; set; }
    }
}
