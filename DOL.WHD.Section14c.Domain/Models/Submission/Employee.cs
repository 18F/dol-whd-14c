using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PrimaryDisabilityId { get; set; }
        public virtual Response PrimaryDisability { get; set; }

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
