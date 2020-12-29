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
    public class FaresController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/Fares
        public IQueryable<Fare> GetFares()
        {
            return db.Fares;
        }

        // GET: api/Fares/5
        [ResponseType(typeof(Fare))]
        public async Task<IHttpActionResult> GetFare(int id)
        {
            Fare fare = await db.Fares.FindAsync(id);
            if (fare == null)
            {
                return NotFound();
            }

            return Ok(fare);
        }

        // PUT: api/Fares/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFare(int id, Fare fare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fare.FareId)
            {
                return BadRequest();
            }

            db.Entry(fare).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FareExists(id))
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

        // POST: api/Fares
        [ResponseType(typeof(Fare))]
        public async Task<IHttpActionResult> PostFare(Fare fare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fares.Add(fare);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FareExists(fare.FareId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = fare.FareId }, fare);
        }

        // DELETE: api/Fares/5
        [ResponseType(typeof(Fare))]
        public async Task<IHttpActionResult> DeleteFare(int id)
        {
            Fare fare = await db.Fares.FindAsync(id);
            if (fare == null)
            {
                return NotFound();
            }

            db.Fares.Remove(fare);
            await db.SaveChangesAsync();

            return Ok(fare);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FareExists(int id)
        {
            return db.Fares.Count(e => e.FareId == id) > 0;
        }
    }
}