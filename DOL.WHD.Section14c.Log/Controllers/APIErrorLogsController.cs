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
        public IHttpActionResult GetErrorLogByID(int id)
        {
            var logs = errorLogRepository.GetAllLogs().FirstOrDefault((p) => p.Id == id);
            if (logs == null)
            {
                return NotFound();
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
        public IHttpActionResult AddLog(LogDetails errorLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var log = errorLogRepository.AddLog(errorLog);
                if (log == null)
                {
                    var message = string.Format("Unable to add log");
                    throw new HttpResponseException(
                        Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, message));
                }
                else
                {
                    return Ok(log);
                }
            }
            catch(Exception ex)
            {
                throw new HttpResponseException(
                        Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
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
