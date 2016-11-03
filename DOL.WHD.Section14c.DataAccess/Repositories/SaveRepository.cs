using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;

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

        public IQueryable<Attachment> GetAttachments()
        {
            return _dbContext.FileUploads.AsQueryable();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
