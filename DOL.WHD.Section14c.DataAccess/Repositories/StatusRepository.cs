using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class StatusRepository: IStatusRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private Boolean Disposed = false;

        public StatusRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<Status> Get()
        {
            return _dbContext.ApplicationStatuses.AsQueryable();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _dbContext.Dispose();
                Disposed = true;
            }
        }
    }
}
