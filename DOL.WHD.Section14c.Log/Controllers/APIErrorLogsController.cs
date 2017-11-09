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
using DOL.WHD.Section14c.Log.DataAccess.Repositories;
using DOL.WHD.Section14c.Log.ActionFilters;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.Log.Controllers
{
    [RoutePrefix("api/errorlogs")]
    public class ErrorLogsController : BaseApiController
    {
        private IErrorLogRepository errorLogRepository;


        public ErrorLogsController(IErrorLogRepository repository)
        {
            errorLogRepository = repository;
        }

        /// <summary>
        /// Gets a list of error logs
        /// </summary>
        /// <returns></returns>
       
        [HttpGet]
        [Route("getalllogs")]
        [LoggingFilterAttribute]
        [GlobalExceptionAttribute]
        public IQueryable<APIErrorLogs> GetAllLogs()
        {
            var logs = errorLogRepository.GetAllLogs();
            
            if (logs == null){
                NotFound("Log not found");
            }
            return logs;
        }

        /// <summary>
        /// Get an error log by id
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        // GET: api/ErrorLogs/5
        [HttpGet]
        [ResponseType(typeof(APIErrorLogs))]
        [Route("getlogbyid")]
        [LoggingFilterAttribute]
        [GlobalExceptionAttribute]
        public IHttpActionResult GetErrorLogByID(string correlationId)
        {
            var logs = errorLogRepository.GetAllLogs().FirstOrDefault((p) => p.CorrelationId == correlationId);
            if (logs == null)
            {
                NotFound("Log not found");
            }
            return Ok(logs);
        }


        /// <summary>
        /// Add a new error log
        /// </summary>
        /// <returns></returns>
        // POST: api/ErrorLogs
        [HttpPost]
        [Route("addlog")]
        [LoggingFilterAttribute]
        [GlobalExceptionAttribute]
        public IHttpActionResult AddLog(LogDetails errorLog)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Model State is not valid");
            }
           
            var log = errorLogRepository.AddLog(errorLog);

            if (log == null)
            {
                ExpectationFailed("Unable to add log");
            }

            return Ok(log);     
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            errorLogRepository?.Dispose();

            base.Dispose(disposing);
        }        
    }
}
