namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class PieceRateWageInfo : WageTypeInfo
    {
        public string PieceRateWorkDescription { get; set; }

        public double? PrevailingWageDeterminedForJob { get; set; }

        public double? StandardProductivity { get; set; }

        public double? PieceRatePaidToWorkers { get; set; }
    }
}
