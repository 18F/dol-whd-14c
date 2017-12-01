using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class EmployerRepository: IEmployerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private bool Disposed = false;

        public EmployerRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Employer> Get()
        {
            return _dbContext.Employers.AsQueryable();
        }

        public async Task<int> AddAsync(Employer employer)
        {
            _dbContext.Employers.Add(employer);
            return await SaveChangesAsync();
        }

        public async Task<int> ModifyEmployer(Employer employer)
        {
            _dbContext.Entry(employer).State = EntityState.Modified;
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

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
