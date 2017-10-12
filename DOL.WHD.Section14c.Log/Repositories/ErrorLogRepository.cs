using DOL.WHD.Section14c.Log.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DOL.WHD.Section14c.Log.Repositories
{
    public class ErrorLogRepository: IErrorLogRepository
    {
        private readonly ApplicationLogContext _dbContext;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private Boolean Disposed;

        public ErrorLogRepository()
        {
            _dbContext = new ApplicationLogContext();
        }

        public IQueryable<APIErrorLogs> GetAllLogs()
        {
            return _dbContext.ErrorLogs.AsQueryable();
        }

        public async Task<APIErrorLogs> GetActivityLogByIDAsync(int id)
        {
            return await _dbContext.ErrorLogs.FindAsync(id);
        }

        public APIErrorLogs AddLog(APIErrorLogs entity)
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
                }
                
                eventInfo.Level = LogLevel.FromString(entity.Level);
                eventInfo.Properties["UserId"] = entity.UserId;
                eventInfo.Properties["UserName"] = entity.User;
                _logger.Log(eventInfo);
            }
            return entity;
        }
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