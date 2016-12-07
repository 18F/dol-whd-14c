using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Signature : BaseEntity
    {
        public Signature()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public bool? Agreement { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
