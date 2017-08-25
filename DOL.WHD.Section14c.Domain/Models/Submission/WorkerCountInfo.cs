using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WorkerCountInfo : BaseEntity
    {
        public WorkerCountInfo()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int? Total { get; set; }

        public int? WorkCenter { get; set; }

        public int? PatientWorkers { get; set; }

        public int? SWEP { get; set; }

        public int? BusinessEstablishment { get; set; }
    }
}
