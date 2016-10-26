using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class PieceRateWageInfoDto : WageTypeInfoDto
    {
        [Required]
        public string PieceRateWorkDescription { get; set; }

        [Required]
        public double PrevailingWageDeterminedForJob { get; set; }

        [Required]
        public double StandardProductivity { get; set; }

        [Required]
        public double PieceRatePaidToWorkers { get; set; }
    }
}
