using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DOL.WHD.Section14c.Log.DataAccess.Models;
using DOL.WHD.Section14c.Log.DataAccess;

namespace DOL.WHD.Section14c.Log.DataAccess.Repositories
{
    /// <summary>
    /// Activity Log Repository
    /// </summary>
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly ApplicationLogContext _dbContext;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();        
        
        private Boolean Disposed;

        /// <summary>
        /// Activity Log Repository
        /// </summary>
        public ActivityLogRepository()
        {
            _dbContext = new ApplicationLogContext();
        }

        /// <summary>
        /// Get ALl Logs
        /// </summary>
        /// <returns></returns>
        public IQueryable<APIActivityLogs> GetAllLogs()
        {
            return _dbContext.ActivityLogs.AsQueryable();
        }

        /// <summary>
        /// Dispose Object
        /// </summary>
        public void Dispose()
        {
            if (!Disposed)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    Disposed = true;
                }
            }
        }
    }
}