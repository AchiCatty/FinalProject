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
    public class TransactionTypesController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/TransactionTypes
        public IQueryable<TransactionType> GetTransactionTypes()
        {
            return db.TransactionTypes;
        }

        // GET: api/TransactionTypes/5
        [ResponseType(typeof(TransactionType))]
        public async Task<IHttpActionResult> GetTransactionType(int id)
        {
            TransactionType transactionType = await db.TransactionTypes.FindAsync(id);
            if (transactionType == null)
            {
                return NotFound();
            }

            return Ok(transactionType);
        }

        // PUT: api/TransactionTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransactionType(int id, TransactionType transactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transactionType.TransactionTypesId)
            {
                return BadRequest();
            }

            db.Entry(transactionType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionTypeExists(id))
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

        // POST: api/TransactionTypes
        [ResponseType(typeof(TransactionType))]
        public async Task<IHttpActionResult> PostTransactionType(TransactionType transactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransactionTypes.Add(transactionType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TransactionTypeExists(transactionType.TransactionTypesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = transactionType.TransactionTypesId }, transactionType);
        }

        // DELETE: api/TransactionTypes/5
        [ResponseType(typeof(TransactionType))]
        public async Task<IHttpActionResult> DeleteTransactionType(int id)
        {
            TransactionType transactionType = await db.TransactionTypes.FindAsync(id);
            if (transactionType == null)
            {
                return NotFound();
            }

            db.TransactionTypes.Remove(transactionType);
            await db.SaveChangesAsync();

            return Ok(transactionType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionTypeExists(int id)
        {
            return db.TransactionTypes.Count(e => e.TransactionTypesId == id) > 0;
        }
    }
}