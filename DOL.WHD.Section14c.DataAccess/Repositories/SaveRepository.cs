using System.Data.Entity.Migrations;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class SaveRepository : ISaveRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IQueryable<ApplicationSave> Get()
        {
            return _dbContext.ApplicationSaves.AsQueryable();
        }

        public void Add(ApplicationSave applicationSave)
        {
            _dbContext.ApplicationSaves.Add(applicationSave);
            SaveChanges();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
