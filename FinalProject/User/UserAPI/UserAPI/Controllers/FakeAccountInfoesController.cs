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
    public class FakeAccountInfoesController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/FakeAccountInfoes
        public IQueryable<FakeAccountInfo> GetFakeAccountInfoes()
        {
            return db.FakeAccountInfoes;
        }

        // GET: api/FakeAccountInfoes/5
        [ResponseType(typeof(FakeAccountInfo))]
        public async Task<IHttpActionResult> GetFakeAccountInfo(int id)
        {
            FakeAccountInfo fakeAccountInfo = await db.FakeAccountInfoes.FindAsync(id);
            if (fakeAccountInfo == null)
            {
                return NotFound();
            }

            return Ok(fakeAccountInfo);
        }

        // PUT: api/FakeAccountInfoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFakeAccountInfo(int id, FakeAccountInfo fakeAccountInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fakeAccountInfo.InfoId)
            {
                return BadRequest();
            }

            db.Entry(fakeAccountInfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FakeAccountInfoExists(id))
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

        // POST: api/FakeAccountInfoes
        [ResponseType(typeof(FakeAccountInfo))]
        public async Task<IHttpActionResult> PostFakeAccountInfo(FakeAccountInfo fakeAccountInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FakeAccountInfoes.Add(fakeAccountInfo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fakeAccountInfo.InfoId }, fakeAccountInfo);
        }

        // DELETE: api/FakeAccountInfoes/5
        [ResponseType(typeof(FakeAccountInfo))]
        public async Task<IHttpActionResult> DeleteFakeAccountInfo(int id)
        {
            FakeAccountInfo fakeAccountInfo = await db.FakeAccountInfoes.FindAsync(id);
            if (fakeAccountInfo == null)
            {
                return NotFound();
            }

            db.FakeAccountInfoes.Remove(fakeAccountInfo);
            await db.SaveChangesAsync();

            return Ok(fakeAccountInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FakeAccountInfoExists(int id)
        {
            return db.FakeAccountInfoes.Count(e => e.InfoId == id) > 0;
        }
    }
}