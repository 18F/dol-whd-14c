using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class HourlyWageInfo : WageTypeInfo
    {
        [Required]
        public string FrequencyOfWorkMeasurements { get; set; }

        // TODO: upload work measurement
    }
}
