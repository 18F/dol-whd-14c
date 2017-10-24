using System;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Log.DataAccess.Models;

namespace DOL.WHD.Section14c.Log.DataAccess.Repositories
{
    /// <summary>
    /// Activity Log Interface
    /// </summary>
    public interface IActivityLogRepository : IDisposable
    {
        /// <summary>
        /// Get all Activity Logs
        /// </summary>
        /// <returns></returns>
        IQueryable<APIActivityLogs> GetAllLogs();      
    }
}