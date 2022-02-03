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
    public class FavoritesController : ApiController
    {
        private FlowerAppEntities db = new FlowerAppEntities();

        // GET: api/Favorites
        public IQueryable<tbl_favorites> Gettbl_favorites()
        {
            return db.tbl_favorites;
        }

        // GET: api/Favorites/5
        [ResponseType(typeof(tbl_favorites))]
        public IHttpActionResult Gettbl_favorites(int id)
        {
            tbl_favorites tbl_favorites = db.tbl_favorites.Find(id);
            if (tbl_favorites == null)
            {
                return NotFound();
            }

            return Ok(tbl_favorites);
        }

        // PUT: api/Favorites/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_favorites(int id, tbl_favorites tbl_favorites)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_favorites.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_favorites).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_favoritesExists(id))
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

        // POST: api/Favorites
        [ResponseType(typeof(tbl_favorites))]
        public IHttpActionResult Posttbl_favorites(tbl_favorites tbl_favorites)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach(tbl_favorites fav in db.tbl_favorites)
            {
                if(fav.product_ID == tbl_favorites.product_ID && fav.username_ID == tbl_favorites.username_ID)
                {
                    //fav.quantity += 1;
                    //Puttbl_favorites(fav.ID, fav);
                    //fav.quantity += 1;
                    //db.SaveChanges();
                    return Ok("You already selected this product, if you need more, please go to the cart;");
                }
               
            }

            db.tbl_favorites.Add(tbl_favorites);
            db.SaveChanges();
            return Ok("Product added");
        }

        // DELETE: api/Favorites/5
        [ResponseType(typeof(tbl_favorites))]
        public IHttpActionResult Deletetbl_favorites(int id)
        {
            tbl_favorites tbl_favorites = db.tbl_favorites.Find(id);
            if (tbl_favorites == null)
            {
                return NotFound();
            }

            db.tbl_favorites.Remove(tbl_favorites);
            db.SaveChanges();

            return Ok(tbl_favorites);
            /*
            var products = db.tbl_favorites.Where(x => x.username_ID.Equals(id)).ToList();
            if (products == null)
            {
                return NotFound();
            }
            foreach(var pr in products)
            {
                db.tbl_favorites.Remove(pr);

            }
            db.SaveChanges();
            return Ok("The favorite stock for this user now is empty");*/
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_favoritesExists(int id)
        {
            return db.tbl_favorites.Count(e => e.ID == id) > 0;
        }
        [Route("api/Favorites/Search")]
        [HttpGet]
        public IHttpActionResult GetProductsbyIDUser(int id)
        {
            try {
                var ProductsAssociated = new List<ProductsFavs>();
                var products = db.tbl_favorites.Where(x => x.username_ID.Equals(id)).ToList();
                foreach (var pr in products)
                {
                    tbl_products prod = db.tbl_products.Where(d => d.ID.Equals(pr.product_ID)).FirstOrDefault();
                    ProductsFavs fav = new ProductsFavs(prod.Code_product, prod.Name, prod.Price, prod.Description, prod.ID, pr.ID,pr.quantity,pr.username_ID);
                    ProductsAssociated.Add(fav);
                }

                if (ProductsAssociated != null)
                {
                    return Ok(ProductsAssociated);
                }
                else
                {
                    return NotFound();
                }


            }catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest("There is a problem retrieving the data");
            }
        }




    }
}