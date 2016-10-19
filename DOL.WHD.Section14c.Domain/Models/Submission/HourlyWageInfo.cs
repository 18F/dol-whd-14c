using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class HourlyWageInfo : WageTypeInfo
    {
        [Required]
        public string WorkMeasurementFrequency { get; set; }
    }
}
