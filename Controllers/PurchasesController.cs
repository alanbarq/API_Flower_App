using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiFlowerTwo.Models;

namespace WebApiFlowerTwo.Controllers
{
    public class PurchasesController : ApiController
    {
        private FlowerAppEntities db = new FlowerAppEntities();

        // GET: api/Purchases
        public IQueryable<tbl_purchases> Gettbl_purchases()
        {
            return db.tbl_purchases;
        }

        // GET: api/Purchases/5
        [ResponseType(typeof(tbl_purchases))]
        public IHttpActionResult Gettbl_purchases(int id)
        {
            tbl_purchases tbl_purchases = db.tbl_purchases.Find(id);
            if (tbl_purchases == null)
            {
                return NotFound();
            }

            return Ok(tbl_purchases);
        }

        // PUT: api/Purchases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_purchases(int id, tbl_purchases tbl_purchases)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_purchases.PurchaseID)
            {
                return BadRequest();
            }

            db.Entry(tbl_purchases).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_purchasesExists(id))
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

        // POST: api/Purchases
        [ResponseType(typeof(tbl_purchases))]
        public IHttpActionResult Posttbl_purchases(tbl_purchases tbl_purchases)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_purchases.Add(tbl_purchases);
            db.SaveChanges();

            return Ok(tbl_purchases);
            //return CreatedAtRoute("DefaultApi", new { id = tbl_purchases.PurchaseID }, tbl_purchases);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_purchasesExists(int id)
        {
            return db.tbl_purchases.Count(e => e.PurchaseID == id) > 0;
        }
    }
}