using System;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Log.Models;

namespace DOL.WHD.Section14c.Log.Repositories
{
    public interface IActivityLogRepository : IDisposable
    {
        IQueryable<APIActivityLogs> GetAllLogs();

        Task<APIActivityLogs> GetActivityLogByIDAsync(int id);

        LogDetails AddLog(LogDetails entity);

    }
}