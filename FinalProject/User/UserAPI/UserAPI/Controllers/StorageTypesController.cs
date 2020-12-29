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
    public class StorageTypesController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/StorageTypes
        public IQueryable<StorageType> GetStorageTypes()
        {
            return db.StorageTypes;
        }

        // GET: api/StorageTypes/5
        [ResponseType(typeof(StorageType))]
        public async Task<IHttpActionResult> GetStorageType(int id)
        {
            StorageType storageType = await db.StorageTypes.FindAsync(id);
            if (storageType == null)
            {
                return NotFound();
            }

            return Ok(storageType);
        }

        // PUT: api/StorageTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStorageType(int id, StorageType storageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storageType.StorageTypeId)
            {
                return BadRequest();
            }

            db.Entry(storageType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageTypeExists(id))
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

        // POST: api/StorageTypes
        [ResponseType(typeof(StorageType))]
        public async Task<IHttpActionResult> PostStorageType(StorageType storageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StorageTypes.Add(storageType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StorageTypeExists(storageType.StorageTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = storageType.StorageTypeId }, storageType);
        }

        // DELETE: api/StorageTypes/5
        [ResponseType(typeof(StorageType))]
        public async Task<IHttpActionResult> DeleteStorageType(int id)
        {
            StorageType storageType = await db.StorageTypes.FindAsync(id);
            if (storageType == null)
            {
                return NotFound();
            }

            db.StorageTypes.Remove(storageType);
            await db.SaveChangesAsync();

            return Ok(storageType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StorageTypeExists(int id)
        {
            return db.StorageTypes.Count(e => e.StorageTypeId == id) > 0;
        }
    }
}