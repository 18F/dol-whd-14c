using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class PieceRateWageInfo : WageTypeInfo
    {
        [Required]
        public int NumPieceRateWorkers { get; set; }

        [Required]
        public string WorkDescription { get; set; }

        [Required]
        public double WageRatePerHour { get; set; }

        [Required]
        public double ProductivityUnitsPerHour { get; set; }

        [Required]
        public double PieceRatePerUnit { get; set; }

        // Documentation
        public Attachment Attachment { get; set; }
    }
}
