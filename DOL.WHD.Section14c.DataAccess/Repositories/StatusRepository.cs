using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class StatusRepository: IStatusRepository
    {
        private readonly ApplicationDbContext _dbContext;
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
            _dbContext.Dispose();
        }
    }
}
