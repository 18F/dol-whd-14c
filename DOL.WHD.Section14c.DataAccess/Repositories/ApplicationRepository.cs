using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private bool Disposed;

        public ApplicationRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<ApplicationSubmission> Get()
        {
            return _dbContext.ApplicationSubmissions.AsQueryable();
        }

        public async Task<int> AddAsync(ApplicationSubmission submission)
        {
            _dbContext.ApplicationSubmissions.Add(submission);
            return await SaveChangesAsync();
        }

        public async Task<int> ModifyApplication(ApplicationSubmission submission)
        {
            _dbContext.Entry(submission).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
