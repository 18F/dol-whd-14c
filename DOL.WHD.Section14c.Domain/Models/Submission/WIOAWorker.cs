using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WIOAWorker : BaseEntity
    {
        public WIOAWorker()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? WIOAWorkerVerifiedId { get; set; }
        public virtual Response WIOAWorkerVerified { get; set; }
    }
}
