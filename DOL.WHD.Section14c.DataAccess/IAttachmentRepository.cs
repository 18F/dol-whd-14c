using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IAttachmentRepository : IDisposable
    {
        IQueryable<Attachment> Get();
        void Add(Attachment attachment);
        int SaveChanges();
    }
}