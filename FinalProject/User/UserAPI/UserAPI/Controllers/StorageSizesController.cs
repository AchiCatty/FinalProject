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
    public class StorageSizesController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/StorageSizes
        public IQueryable<StorageSize> GetStorageSizes()
        {
            return db.StorageSizes;
        }

        // GET: api/StorageSizes/5
        [ResponseType(typeof(StorageSize))]
        public async Task<IHttpActionResult> GetStorageSize(int id)
        {
            StorageSize storageSize = await db.StorageSizes.FindAsync(id);
            if (storageSize == null)
            {
                return NotFound();
            }

            return Ok(storageSize);
        }

        // PUT: api/StorageSizes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStorageSize(int id, StorageSize storageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storageSize.StorageSizeId)
            {
                return BadRequest();
            }

            db.Entry(storageSize).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageSizeExists(id))
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

        // POST: api/StorageSizes
        [ResponseType(typeof(StorageSize))]
        public async Task<IHttpActionResult> PostStorageSize(StorageSize storageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StorageSizes.Add(storageSize);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StorageSizeExists(storageSize.StorageSizeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = storageSize.StorageSizeId }, storageSize);
        }

        // DELETE: api/StorageSizes/5
        [ResponseType(typeof(StorageSize))]
        public async Task<IHttpActionResult> DeleteStorageSize(int id)
        {
            StorageSize storageSize = await db.StorageSizes.FindAsync(id);
            if (storageSize == null)
            {
                return NotFound();
            }

            db.StorageSizes.Remove(storageSize);
            await db.SaveChangesAsync();

            return Ok(storageSize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StorageSizeExists(int id)
        {
            return db.StorageSizes.Count(e => e.StorageSizeId == id) > 0;
        }
    }
}