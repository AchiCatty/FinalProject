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
    public class CustomerTypesController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/CustomerTypes
        public IQueryable<CustomerType> GetCustomerTypes()
        {
            return db.CustomerTypes;
        }

        // GET: api/CustomerTypes/5
        [ResponseType(typeof(CustomerType))]
        public async Task<IHttpActionResult> GetCustomerType(int id)
        {
            CustomerType customerType = await db.CustomerTypes.FindAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }

            return Ok(customerType);
        }

        // PUT: api/CustomerTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomerType(int id, CustomerType customerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerType.CustomerTypeId)
            {
                return BadRequest();
            }

            db.Entry(customerType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerTypeExists(id))
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

        // POST: api/CustomerTypes
        [ResponseType(typeof(CustomerType))]
        public async Task<IHttpActionResult> PostCustomerType(CustomerType customerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerTypes.Add(customerType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerTypeExists(customerType.CustomerTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customerType.CustomerTypeId }, customerType);
        }

        // DELETE: api/CustomerTypes/5
        [ResponseType(typeof(CustomerType))]
        public async Task<IHttpActionResult> DeleteCustomerType(int id)
        {
            CustomerType customerType = await db.CustomerTypes.FindAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }

            db.CustomerTypes.Remove(customerType);
            await db.SaveChangesAsync();

            return Ok(customerType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerTypeExists(int id)
        {
            return db.CustomerTypes.Count(e => e.CustomerTypeId == id) > 0;
        }
    }
}