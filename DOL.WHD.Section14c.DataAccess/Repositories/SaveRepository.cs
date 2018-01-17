using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class SaveRepository : ISaveRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private Boolean Disposed = false;

        public SaveRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<ApplicationSave> Get()
        {
            return _dbContext.ApplicationSaves.AsQueryable();
        }

        public void AddOrUpdate(ApplicationSave applicationSave)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.ApplicationSaves.AddOrUpdate(applicationSave);
                    SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void Remove(string applicationId)
        {
            var save = _dbContext.ApplicationSaves.SingleOrDefault(x => x.ApplicationId == applicationId);
            if (save != null)
            {
                _dbContext.ApplicationSaves.Remove(save);
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
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
