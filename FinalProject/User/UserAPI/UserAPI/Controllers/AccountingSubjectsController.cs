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
    public class AccountingSubjectsController : ApiController
    {
        private 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities db = new 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities();

        // GET: api/AccountingSubjects
        public IQueryable<AccountingSubject> GetAccountingSubjects()
        {
            return db.AccountingSubjects;
        }

        // GET: api/AccountingSubjects/5
        [ResponseType(typeof(AccountingSubject))]
        public async Task<IHttpActionResult> GetAccountingSubject(int id)
        {
            AccountingSubject accountingSubject = await db.AccountingSubjects.FindAsync(id);
            if (accountingSubject == null)
            {
                return NotFound();
            }

            return Ok(accountingSubject);
        }

        // PUT: api/AccountingSubjects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAccountingSubject(int id, AccountingSubject accountingSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountingSubject.AccountingSubjectId)
            {
                return BadRequest();
            }

            db.Entry(accountingSubject).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingSubjectExists(id))
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

        // POST: api/AccountingSubjects
        [ResponseType(typeof(AccountingSubject))]
        public async Task<IHttpActionResult> PostAccountingSubject(AccountingSubject accountingSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountingSubjects.Add(accountingSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountingSubjectExists(accountingSubject.AccountingSubjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accountingSubject.AccountingSubjectId }, accountingSubject);
        }

        // DELETE: api/AccountingSubjects/5
        [ResponseType(typeof(AccountingSubject))]
        public async Task<IHttpActionResult> DeleteAccountingSubject(int id)
        {
            AccountingSubject accountingSubject = await db.AccountingSubjects.FindAsync(id);
            if (accountingSubject == null)
            {
                return NotFound();
            }

            db.AccountingSubjects.Remove(accountingSubject);
            await db.SaveChangesAsync();

            return Ok(accountingSubject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountingSubjectExists(int id)
        {
            return db.AccountingSubjects.Count(e => e.AccountingSubjectId == id) > 0;
        }
    }
}