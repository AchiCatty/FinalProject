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
    public class PurchaseItemsController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/PurchaseItems
        public IQueryable<PurchaseItem> GetPurchaseItems()
        {
            return db.PurchaseItems;
        }

        // GET: api/PurchaseItems/5
        [ResponseType(typeof(PurchaseItem))]
        public async Task<IHttpActionResult> GetPurchaseItem(int id)
        {
            PurchaseItem purchaseItem = await db.PurchaseItems.FindAsync(id);
            if (purchaseItem == null)
            {
                return NotFound();
            }

            return Ok(purchaseItem);
        }

        // PUT: api/PurchaseItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPurchaseItem(int id, PurchaseItem purchaseItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseItem.PurchaseItemId)
            {
                return BadRequest();
            }

            db.Entry(purchaseItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseItemExists(id))
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

        // POST: api/PurchaseItems
        [ResponseType(typeof(PurchaseItem))]
        public async Task<IHttpActionResult> PostPurchaseItem(PurchaseItem purchaseItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseItems.Add(purchaseItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseItemExists(purchaseItem.PurchaseItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = purchaseItem.PurchaseItemId }, purchaseItem);
        }

        // DELETE: api/PurchaseItems/5
        [ResponseType(typeof(PurchaseItem))]
        public async Task<IHttpActionResult> DeletePurchaseItem(int id)
        {
            PurchaseItem purchaseItem = await db.PurchaseItems.FindAsync(id);
            if (purchaseItem == null)
            {
                return NotFound();
            }

            db.PurchaseItems.Remove(purchaseItem);
            await db.SaveChangesAsync();

            return Ok(purchaseItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseItemExists(int id)
        {
            return db.PurchaseItems.Count(e => e.PurchaseItemId == id) > 0;
        }
    }
}