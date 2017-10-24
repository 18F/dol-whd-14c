using DOL.WHD.Section14c.Log.Models;
using System;
using System.Linq;

namespace DOL.WHD.Section14c.Log.DataAccess.Repositories
{
    public interface IErrorLogRepository : IDisposable
    {
        IQueryable<APIErrorLogs> GetAllLogs();

        LogDetails AddLog(LogDetails entity);
    }
}