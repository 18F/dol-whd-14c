using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IStatusRepository : IDisposable
    {
        IQueryable<Status> Get();
    }
}
