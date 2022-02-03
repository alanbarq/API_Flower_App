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
    public class CreditCardController : ApiController
    {
        private FlowerAppEntities db = new FlowerAppEntities();


        [ResponseType(typeof(tbl_creditCard))]
        public IHttpActionResult GetCCardsbyUser(int user)
        {
            try
            {
                var cardsAssociated = db.tbl_creditCard.Where(x => x.ID_user.Equals(user)).ToList();
                if (cardsAssociated != null)
                {
                    return Ok(cardsAssociated);
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

        [ResponseType(typeof(tbl_creditCard))]
        [HttpPost]
        public IHttpActionResult PostCreditCards(tbl_creditCard tbl_creditCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_creditCard.Add(tbl_creditCard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_creditCard.id_creditcard }, tbl_creditCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_creditCardExists(int id)
        {
            return db.tbl_creditCard.Count(e => e.id_creditcard == id) > 0;
        }
    }
}