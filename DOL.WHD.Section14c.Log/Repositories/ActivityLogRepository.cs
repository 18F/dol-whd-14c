using DOL.WHD.Section14c.Log.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.Http.Tracing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DOL.WHD.Section14c.Log.Helpers;

namespace DOL.WHD.Section14c.Log.Repositories
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
        /// Add New Log
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public LogDetails AddLog(LogDetails entity)
        {
            if (entity != null)
            {
                LogEventInfo eventInfo = new LogEventInfo();

                eventInfo.Properties["EIN"] = string.IsNullOrEmpty(entity.EIN) ? string.Empty : entity.EIN;
                eventInfo.LoggerName = "NLog";
                if (string.IsNullOrEmpty(entity.Message))
                {
                    throw new ArgumentException("Message cannot be null or empty string", "Log Message");
                }

                eventInfo.Message = entity.Message;

                if (!string.IsNullOrEmpty(entity.Exception))
                {
                    eventInfo.Exception = new Exception(entity.Exception);
                    eventInfo.SetStackTrace((new System.Diagnostics.StackTrace(new Exception(entity.Exception), false)), 1);
                }

                
                eventInfo.Level = LogLevel.FromString(entity.Level);
                eventInfo.Properties["UserId"] = entity.UserId;
                eventInfo.Properties["UserName"] = entity.User;
                _logger.Log(eventInfo);
            }
            return entity;
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