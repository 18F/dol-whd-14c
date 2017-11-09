using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private bool Disposed = false;

        public AttachmentRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<Attachment> Get()
        {
            return _dbContext.FileUploads.AsQueryable();
        }

        public void Add(Attachment attachment)
        {
            _dbContext.FileUploads.Add(attachment);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
