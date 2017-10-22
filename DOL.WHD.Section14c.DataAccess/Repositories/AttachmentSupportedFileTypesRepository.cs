using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class AttachmentSupportedFileTypesRepository : IAttachmentSupportedFileTypesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AttachmentSupportedFileTypesRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<AttachmentSupportedFileTypes> Get()
        {
            return _dbContext.ApplicationAttachmentSupportedFileTypes.AsQueryable();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
