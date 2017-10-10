using DOL.WHD.Section14c.Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Log.Repositories
{
    public interface IErrorLogRepository : IDisposable
    {
        IQueryable<APIErrorLogs> GetAllLogs();

        Task<APIErrorLogs> GetActivityLogByIDAsync(int id);

        APIErrorLogs AddLog(APIErrorLogs entity);
    }
}