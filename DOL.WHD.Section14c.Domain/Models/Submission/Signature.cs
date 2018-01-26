using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Signature : BaseEntity
    {
        public Signature()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public bool? Agreement { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
