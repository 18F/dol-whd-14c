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
using DOL.WHD.Section14c.Log.Repositories;

namespace DOL.WHD.Section14c.Log.Controllers
{
    [RoutePrefix("api/ErrorLogs")]
    public class ErrorLogsController : ApiController
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
        [Route("GetAllLogs")]
        public IQueryable<APIErrorLogs> GetAllLogs()
        {
            var logs = errorLogRepository.GetAllLogs();
            
            if (logs == null)
            {
                var message = string.Format("activity Log not found");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            else
            {
                return logs;
            }
        }

        /// <summary>
        /// Get an error log by id
        /// </summary>
        /// <returns></returns>
        // GET: api/ErrorLogs/5
        [HttpGet]
        [ResponseType(typeof(APIErrorLogs))]
        [Route("GetLogByID")]
        public async Task<IHttpActionResult> GetErrorLogByID(int id)
        {
            APIErrorLogs logs = await errorLogRepository.GetActivityLogByIDAsync(id);
            if (logs == null)
            {
                var message = string.Format("activity Log not found");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            else
            {
                return Ok(logs);
            }
        }


        /// <summary>
        /// Add a new error log
        /// </summary>
        /// <returns></returns>
        // POST: api/ErrorLogs
        [HttpPost]
        [Route("AddLog")]
        //[ResponseType(typeof(APIErrorLogs))]
        public async Task<IHttpActionResult> AddLog(APIErrorLogs errorLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            APIErrorLogs log = await errorLogRepository.AddLogAsync(errorLog);
            if (log == null)
            {
                var message = string.Format("unable to add log");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, message));
            }
            else
            {
                return Ok(log);
            }            
        }


        protected override void Dispose(bool disposing)
        {
            errorLogRepository?.Dispose();

            base.Dispose(disposing);
        }        
    }
}
