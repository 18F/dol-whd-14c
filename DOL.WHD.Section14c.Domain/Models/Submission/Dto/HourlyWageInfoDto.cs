using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class HourlyWageInfoDto : WageTypeInfoDto
    {
        [Required]
        public string WorkMeasurementFrequency { get; set; }
    }
}
