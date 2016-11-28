using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Identity;

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

        public void Remove(string EIN)
        {
            var save = _dbContext.ApplicationSaves.SingleOrDefault(x => x.EIN == EIN);
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
            _dbContext.Dispose();
        }
    }
}
