using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int? PrimaryDisabilityId { get; set; }
        public virtual Response PrimaryDisability { get; set; }

        public string PrimaryDisabilityOther { get; set; }

        public string WorkType { get; set; }

        public int? NumJobs { get; set; }

        public double? AvgWeeklyHours { get; set; }

        public double? AvgHourlyEarnings { get; set; }

        public double? PrevailingWage { get; set; }

        public double? ProductivityMeasure { get; set; }

        public string CommensurateWageRate { get; set; }

        public double? TotalHours { get; set; }

        public bool? WorkAtOtherSite { get; set; }
    }
}
