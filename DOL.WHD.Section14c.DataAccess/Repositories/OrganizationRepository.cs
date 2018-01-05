using DOL.WHD.Section14c.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class OrganizationRepository: IOrganizationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private bool Disposed = false;

        public OrganizationRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IEnumerable<OrganizationMembership> Get()
        {
            return _dbContext.OrganizationMemberships.Include("CreatedBy").AsQueryable();
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
