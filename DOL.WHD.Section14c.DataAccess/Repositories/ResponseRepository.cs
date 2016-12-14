using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ResponseRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<Response> Get()
        {
            return _dbContext.Responses.AsQueryable();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
