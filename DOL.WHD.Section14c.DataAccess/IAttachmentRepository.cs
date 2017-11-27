using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IAttachmentRepository : IDisposable
    {
        IEnumerable<Attachment> Get();
        void Add(Attachment attachment);
        int SaveChanges();
    }
}