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
    public class AccountTypesController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/AccountTypes
        public IQueryable<AccountType> GetAccountTypes()
        {
            return db.AccountTypes;
        }

        // GET: api/AccountTypes/5
        [ResponseType(typeof(AccountType))]
        public async Task<IHttpActionResult> GetAccountType(int id)
        {
            AccountType accountType = await db.AccountTypes.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }

            return Ok(accountType);
        }

        // PUT: api/AccountTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAccountType(int id, AccountType accountType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountType.AccountTypeId)
            {
                return BadRequest();
            }

            db.Entry(accountType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountTypeExists(id))
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

        // POST: api/AccountTypes
        [ResponseType(typeof(AccountType))]
        public async Task<IHttpActionResult> PostAccountType(AccountType accountType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountTypes.Add(accountType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountTypeExists(accountType.AccountTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accountType.AccountTypeId }, accountType);
        }

        // DELETE: api/AccountTypes/5
        [ResponseType(typeof(AccountType))]
        public async Task<IHttpActionResult> DeleteAccountType(int id)
        {
            AccountType accountType = await db.AccountTypes.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }

            db.AccountTypes.Remove(accountType);
            await db.SaveChangesAsync();

            return Ok(accountType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountTypeExists(int id)
        {
            return db.AccountTypes.Count(e => e.AccountTypeId == id) > 0;
        }
    }
}