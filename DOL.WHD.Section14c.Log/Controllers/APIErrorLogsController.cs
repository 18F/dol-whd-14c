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

namespace DOL.WHD.Section14c.Log.Controllers
{
    public class ErrorLogsController : ApiController
    {
        private ApplicationLogContext db = new ApplicationLogContext();

        /// <summary>
        /// Gets a list of error logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // GET: api/ErrorLogs
        public IQueryable<APIErrorLogs> GetAllErrorLogs()
        {
            return db.ErrorLogs;
        }

        /// <summary>
        /// Get an error log by id
        /// </summary>
        /// <returns></returns>
        // GET: api/ErrorLogs/5
        [HttpGet]
        [ResponseType(typeof(APIErrorLogs))]
        public async Task<IHttpActionResult> GetErrorLogByID(int id)
        {
            APIErrorLogs aPILogs = await db.ErrorLogs.FindAsync(id);
            if (aPILogs == null)
            {
                return NotFound();
            }

            return Ok(aPILogs);
        }

        /// <summary>
        /// Update error log by id
        /// </summary>
        /// <returns></returns>
        // PUT: api/ErrorLogs/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateErrorLogByID(int id, APIErrorLogs aPILogs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aPILogs.Id)
            {
                return BadRequest();
            }

            db.Entry(aPILogs).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APILogsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Add a new error log
        /// </summary>
        /// <returns></returns>
        // POST: api/ErrorLogs
        [HttpPost]
        [ResponseType(typeof(APIErrorLogs))]
        public async Task<IHttpActionResult> NewErrorLog(APIErrorLogs aPILogs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ErrorLogs.Add(aPILogs);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (APILogsExists(aPILogs.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aPILogs.Id }, aPILogs);
        }

        /// <summary>
        /// Delete an error log by id
        /// </summary>
        /// <returns></returns>
        // DELETE: api/ErrorLogs/5
        [HttpDelete]
        [ResponseType(typeof(APIErrorLogs))]
        public async Task<IHttpActionResult> DeleteErrorLogByID(string id)
        {
            APIErrorLogs aPILogs = await db.ErrorLogs.FindAsync(id);
            if (aPILogs == null)
            {
                return NotFound();
            }

            db.ErrorLogs.Remove(aPILogs);
            await db.SaveChangesAsync();

            return Ok(aPILogs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool APILogsExists(int id)
        {
            return db.ErrorLogs.Count(e => e.Id == id) > 0;
        }
    }
}
