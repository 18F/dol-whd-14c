using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface ISaveRepository : IDisposable
    {
        IQueryable<ApplicationSave> Get();
        void AddOrUpdate(ApplicationSave applicationSave);
    }
}
