using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WIOAWorker : BaseEntity
    {
        public WIOAWorker()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int WIOAWorkerVerifiedId { get; set; }
        public Response WIOAWorkerVerified { get; set; }
    }
}
