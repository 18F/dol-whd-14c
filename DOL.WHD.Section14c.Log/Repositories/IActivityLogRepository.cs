using DOL.WHD.Section14c.Log.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Log.Repositories
{
    public interface IActivityLogRepository : IDisposable
    {
        IQueryable<APIActivityLogs> GetAllLogs();

        Task<APIActivityLogs> GetActivityLogByIDAsync(int id);

        Task<APIActivityLogs> AddLogAsync(APIActivityLogs entity);

    }
}