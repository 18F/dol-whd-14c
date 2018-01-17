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
            return _dbContext.Employers.Include( x => x.PhysicalAddress).AsQueryable();
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
