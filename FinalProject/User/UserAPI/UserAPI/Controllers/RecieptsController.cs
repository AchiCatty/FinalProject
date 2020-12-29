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
using UserLibrary;

namespace UserAPI.Controllers
{
    public class RecieptsController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/Reciepts
        public IQueryable<Reciept> GetReciepts()
        {
            return db.Reciepts;
        }

        // GET: api/Reciepts/5
        [ResponseType(typeof(Reciept))]
        public async Task<IHttpActionResult> GetReciept(int id)
        {
            Reciept reciept = await db.Reciepts.FindAsync(id);
            if (reciept == null)
            {
                return NotFound();
            }

            return Ok(reciept);
        }

        // PUT: api/Reciepts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReciept(int id, Reciept reciept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reciept.RecieptId)
            {
                return BadRequest();
            }

            db.Entry(reciept).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecieptExists(id))
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

        // POST: api/Reciepts
        [ResponseType(typeof(Reciept))]
        public async Task<IHttpActionResult> PostReciept(Reciept reciept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reciepts.Add(reciept);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecieptExists(reciept.RecieptId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reciept.RecieptId }, reciept);
        }

        // DELETE: api/Reciepts/5
        [ResponseType(typeof(Reciept))]
        public async Task<IHttpActionResult> DeleteReciept(int id)
        {
            Reciept reciept = await db.Reciepts.FindAsync(id);
            if (reciept == null)
            {
                return NotFound();
            }

            db.Reciepts.Remove(reciept);
            await db.SaveChangesAsync();

            return Ok(reciept);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecieptExists(int id)
        {
            return db.Reciepts.Count(e => e.RecieptId == id) > 0;
        }
    }
}