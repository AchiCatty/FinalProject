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
    public class StoragesController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/Storages
        public IQueryable<Storage> GetStorages()
        {
            return db.Storages;
        }

        // GET: api/Storages/5
        [ResponseType(typeof(Storage))]
        public async Task<IHttpActionResult> GetStorage(int id)
        {
            Storage storage = await db.Storages.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Storages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStorage(int id, Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storage.StorageId)
            {
                return BadRequest();
            }

            db.Entry(storage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageExists(id))
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

        // POST: api/Storages
        [ResponseType(typeof(Storage))]
        public async Task<IHttpActionResult> PostStorage(Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Storages.Add(storage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StorageExists(storage.StorageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = storage.StorageId }, storage);
        }

        // DELETE: api/Storages/5
        [ResponseType(typeof(Storage))]
        public async Task<IHttpActionResult> DeleteStorage(int id)
        {
            Storage storage = await db.Storages.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }

            db.Storages.Remove(storage);
            await db.SaveChangesAsync();

            return Ok(storage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StorageExists(int id)
        {
            return db.Storages.Count(e => e.StorageId == id) > 0;
        }
    }
}