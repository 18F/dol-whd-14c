
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DOL.WHD.Section14c.Log.Models;
using DOL.WHD.Section14c.Log.DataAccess.Repositories;
using DOL.WHD.Section14c.Log.ActionFilters;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.Log.Controllers
{
    /// <summary>
    /// Log API controller
    /// </summary>
    [RoutePrefix("api/ActivityLogs")]
    public class ActivityLogsController : BaseApiController
    {
        private IActivityLogRepository activityLogRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">
        /// The repository where logs should be stored
        /// </param>
        public ActivityLogsController(IActivityLogRepository repository)
        {
            activityLogRepository = repository;
        }

        /// <summary>
        /// Gets a list of Activity Logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllLogs")]
        public IQueryable<APIActivityLogs> GetAllLogs()
        {
            var activityLogs = activityLogRepository.GetAllLogs();
            
            if (activityLogs == null)
            {
                NotFound("Log not found");
            }
            return activityLogs;
        }

        /// <summary>
        /// Get an activity log by id
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(APIActivityLogs))]
        [Route("GetLogByID")]
        public IHttpActionResult GetActivityLogByID(string correlationId)
        {
            var logs = activityLogRepository.GetAllLogs().FirstOrDefault((p) => p.CorrelationId == correlationId);
            if (logs == null)
            {
                NotFound("Log not found");
            }
            return Ok(logs);
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        /// <param name="disposing"></param>
        /// [LoggingFilterAttribute]
        [GlobalExceptionAttribute]
        protected override void Dispose(bool disposing)
        {
            activityLogRepository?.Dispose();

            base.Dispose(disposing);
        }        
    }
}