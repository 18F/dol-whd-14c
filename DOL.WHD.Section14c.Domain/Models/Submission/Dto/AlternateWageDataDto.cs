using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class AlternateWageDataDto
    {
        public string AlternateWorkDescription { get; set; }
        public string AlternateDataSourceUsed { get; set; }
        public double PrevailingWageProvidedBySource { get; set; }
        public DateTime DataRetrieved { get; set; }
    }
}
