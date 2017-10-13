using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DOL.WHD.Section14c.Log.Models;
using NLog;
using System.Web.Http.Tracing;
using DOL.WHD.Section14c.Log.Helpers;
using DOL.WHD.Section14c.Log.Repositories;

namespace DOL.WHD.Section14c.Log.Controllers
{
    [RoutePrefix("api/ActivityLogs")]
    public class ActivityLogsController : ApiController
    {
        

        private IActivityLogRepository activityLogRepository;

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
                var message = string.Format("activity Log not found");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            else
            {
                return activityLogs;
            }            
        }

        /// <summary>
        /// Get an error log by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(APIActivityLogs))]
        [Route("GetLogByID")]
        public IHttpActionResult GetActivityLogByID(int id)
        {
            var logs = activityLogRepository.GetAllLogs().FirstOrDefault((p) => p.Id == id); 
            if (logs == null)
            {
                return NotFound();               
            }
            else
            {
                return Ok(logs);
            }
        }

        protected override void Dispose(bool disposing)
        {
            activityLogRepository?.Dispose();

            base.Dispose(disposing);
        }        
    }
}