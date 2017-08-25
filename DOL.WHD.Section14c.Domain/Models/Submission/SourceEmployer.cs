using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class SourceEmployer : BaseEntity
    {
        public SourceEmployer()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string EmployerName { get; set; }

        public virtual Address Address { get; set; }

        public string Phone { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public DateTime ContactDate { get; set; }

        public string JobDescription { get; set; }

        public string ExperiencedWorkerWageProvided { get; set; }

        public string ConclusionWageRateNotBasedOnEntry { get; set; }
    }
}
