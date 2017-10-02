using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WIOA : BaseEntity
    {
        public WIOA()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public bool? HasVerifiedDocumentation { get; set; }

        public bool? HasWIOAWorkers { get; set; }

        public virtual ICollection<WIOAWorker> WIOAWorkers { get; set; }

    }
}
