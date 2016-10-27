using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WIOA : BaseEntity
    {
        public WIOA()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public bool HasVerfiedDocumentaion { get; set; }

        public bool HasWIOAWorkers { get; set; }

        public virtual ICollection<WIOAWorker> WIOAWorkers { get; set; }

    }
}
