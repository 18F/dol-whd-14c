using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ApplicationRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<ApplicationSubmission> Get()
        {
            return _dbContext.ApplicationSubmissions.AsQueryable();
        }

        public Task<int> AddAsync(ApplicationSubmission submission)
        {
            _dbContext.ApplicationSubmissions.Add(submission);
            return SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
