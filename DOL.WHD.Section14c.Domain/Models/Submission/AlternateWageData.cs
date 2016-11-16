using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class AlternateWageData : BaseEntity
    {
        public AlternateWageData()
        {
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }

        public string AlternateWorkDescription { get; set; }
        public string AlternateDataSourceUsed { get; set; }
        public double? PrevailingWageProvidedBySource { get; set; }
        public DateTime DataRetrieved { get; set; }
    }
}
