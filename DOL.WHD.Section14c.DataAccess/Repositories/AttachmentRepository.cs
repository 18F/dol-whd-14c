using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
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
            _dbContext.Dispose();
        }
    }
}
