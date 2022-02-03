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
    public class TicketController : ApiController
    {
        private FlowerAppEntities db = new FlowerAppEntities();

        [HttpGet]
        // GET: api/Ticket
        public IHttpActionResult GetticketsbyUser(int user)
        {
            try
            {
                var ticketsAssociated = new List<tbl_ticket>();
                foreach (var ticket in db.tbl_ticket)
                {
                    if (ticket.ID_user == user)
                    {
                        ticketsAssociated.Add(ticket);
                    }

                }
                if (ticketsAssociated != null)
                {
                    return Ok(ticketsAssociated);
                }
                else
                {
                    return BadRequest("There are no tickets associated with the user");
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.StackTrace);
                return BadRequest("There is a problem retrieving the data");
            }

        }
        [HttpGet]
        public IHttpActionResult GetPurchasesbyTicket(int ticket)
        {
            try
            {
                var ProductsAssociated = new List<tbl_products>();
                var purchases = db.tbl_purchases.Where(x => x.ID_ticket.Equals(ticket)).ToList();
                foreach(var purch in purchases)
                {
                    tbl_products prod = db.tbl_products.Where(d => d.Code_product.Equals(purch.Code_product)).FirstOrDefault();
                    ProductsAssociated.Add(prod);
                }
                if (ProductsAssociated != null)
                {
                    return Ok(ProductsAssociated);
                }
                else
                {
                    return BadRequest("There are no products associated with the ticket");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return BadRequest("There is a problem retrieving the data");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ticketExists(int id)
        {
            return db.tbl_ticket.Count(e => e.ID_ticket == id) > 0;
        }

        [ResponseType(typeof(tbl_ticket))]
        public IHttpActionResult Posttbl_ticket(tbl_ticket tbl_ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_ticket.Add(tbl_ticket);
            db.SaveChanges();

            return Ok(tbl_ticket);
        }
    }
}