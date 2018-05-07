using DOL.WHD.Section14c.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private bool Disposed = false;

        public UserActivityRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IEnumerable<UserActivity> Get()
        {
            //explicitly loading related entities 
            return _dbContext.UserActivities.AsQueryable();
        }

        public async Task<int> AddAsync(UserActivity Activity)
        {
            _dbContext.UserActivities.Add(Activity);
            return await SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Add(UserActivity Activity)
        {
            _dbContext.UserActivities.Add(Activity);
            _dbContext.SaveChanges();
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
