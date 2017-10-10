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
    public class ActivityLogsController : ApiController
    {
        private DOLWHDSection14cLogContext db = new DOLWHDSection14cLogContext();

        // GET: api/GetAllActivityLogs
        /// <summary>
        /// Gets a list of Activity Logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IQueryable<APIActivityLogs> GetAllActivityLogs()
        {
            return db.ActivityLogs;
        }

        // GET: api/GetActivityLogByID/5
        /// <summary>
        /// Gets activity log by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(APIActivityLogs))]
        public async Task<IHttpActionResult> GetActivityLogByID(string id)
        {
            APIActivityLogs ActivityLogs = await db.ActivityLogs.FindAsync(id);
            if (ActivityLogs == null)
            {
                return NotFound();
            }

            return Ok(ActivityLogs);
        }

        // PUT: api/UpdateActivityLogByID/5
        /// <summary>
        /// Update activity by id
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateActivityLogByID(int id, APIActivityLogs ActivityLogs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ActivityLogs.Id)
            {
                return BadRequest();
            }

            db.Entry(ActivityLogs).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityLogsExists(id))
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

        // POST: api/NewActivityLog
        /// <summary>
        /// Add new activity log
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(APIActivityLogs))]
        public async Task<IHttpActionResult> NewActivityLog(APIActivityLogs ActivityLogs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivityLogs.Add(ActivityLogs);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActivityLogsExists(ActivityLogs.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ActivityLogs.Id }, ActivityLogs);
        }


        // DELETE: api/DeleteActivityLogByID/5
        /// <summary>
        /// Delete activity log by id
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(APIActivityLogs))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteActivityLogByID(string id)
        {
            APIActivityLogs ActivityLogs = await db.ActivityLogs.FindAsync(id);
            if (ActivityLogs == null)
            {
                return NotFound();
            }

            db.ActivityLogs.Remove(ActivityLogs);
            await db.SaveChangesAsync();

            return Ok(ActivityLogs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityLogsExists(int id)
        {
            return db.ActivityLogs.Count(e => e.Id == id) > 0;
        }
    }
}