using DOL.WHD.Section14c.Log.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DOL.WHD.Section14c.Log.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly ApplicationLogContext _dbContext;
        private readonly Logger _logger;

        private Boolean Disposed;

        public ActivityLogRepository()
        {
            _dbContext = new ApplicationLogContext();
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IQueryable<APIActivityLogs> GetAllLogs()
        {
            return _dbContext.ActivityLogs.AsQueryable();
        }

        public async Task<APIActivityLogs> GetActivityLogByIDAsync(int id)
        {
            return await _dbContext.ActivityLogs.FindAsync(id);
        }

        public async Task<APIActivityLogs> AddLogAsync(APIActivityLogs entity)
        {
            LogEventInfo eventInfo = new LogEventInfo(LogLevel.Error, "NLog", entity.Message);
            eventInfo.Properties["EIN"] = entity.EIN;
            eventInfo.Message = entity.Message;
            //eventInfo.Level = ActivityLogs.Level;
            eventInfo.LoggerName = entity.User;
            _logger.Log(eventInfo);


            //GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            //var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            //trace.Info(filterContext.Request,
            //    "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
            //    Environment.NewLine +
            //    "Action : " + filterContext.ActionDescriptor.ActionName,
            //    "JSON", filterContext.ActionArguments);



            //return CreatedAtRoute("DefaultApi", new { id = ActivityLogs.Id }, ActivityLogs);

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