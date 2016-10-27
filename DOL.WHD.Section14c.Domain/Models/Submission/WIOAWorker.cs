using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WIOAWorker : BaseEntity
    {
        public WIOAWorker()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string FullName { get; set; }

        public int WIOAWorkerVerifiedId { get; set; }
        public Response WIOAWorkerVerified { get; set; }
    }
}
