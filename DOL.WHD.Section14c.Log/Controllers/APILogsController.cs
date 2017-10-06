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
    public class APILogsController : ApiController
    {
        private DOLWHDSection14cLogContext db = new DOLWHDSection14cLogContext();

        // GET: api/APILogs
        public IQueryable<APILogs> GetAPILogs()
        {
            return db.APILogs;
        }

        // GET: api/APILogs/5
        [ResponseType(typeof(APILogs))]
        public async Task<IHttpActionResult> GetAPILogs(string id)
        {
            APILogs aPILogs = await db.APILogs.FindAsync(id);
            if (aPILogs == null)
            {
                return NotFound();
            }

            return Ok(aPILogs);
        }

        // PUT: api/APILogs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAPILogs(string id, APILogs aPILogs)
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

        // POST: api/APILogs
        [ResponseType(typeof(APILogs))]
        public async Task<IHttpActionResult> PostAPILogs(APILogs aPILogs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.APILogs.Add(aPILogs);

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

        // DELETE: api/APILogs/5
        [ResponseType(typeof(APILogs))]
        public async Task<IHttpActionResult> DeleteAPILogs(string id)
        {
            APILogs aPILogs = await db.APILogs.FindAsync(id);
            if (aPILogs == null)
            {
                return NotFound();
            }

            db.APILogs.Remove(aPILogs);
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

        private bool APILogsExists(string id)
        {
            return db.APILogs.Count(e => e.Id == id) > 0;
        }
    }
}