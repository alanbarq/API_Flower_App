
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiFlowerTwo.Models;

namespace WebApiFlowerTwo.Controllers
{
    //[Authorize]
    public class ProductsController : ApiController
    {
        private FlowerAppEntities db = new FlowerAppEntities();

        // GET: api/Products
        public IQueryable<tbl_products> Gettbl_products()
        {
            return db.tbl_products;
        }

        // GET: api/Products/5
        [HttpGet]
        public IHttpActionResult GetProductsByCategory(int category)
        {
            try {
            List<tbl_products> products = new List<tbl_products>();
            foreach (tbl_products pr in db.tbl_products)
            {
                if (pr.ID_Category == category)
                {
                    products.Add(pr);
                }
            }
            if (products.Count != 0)
                {
                    return Ok(products);
                }
                else
                {
                    return BadRequest("There was a problem retrieving the products");
                }
            
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest("Hola mundo");
            }
        }
        [HttpGet]
        public IHttpActionResult SingleProduct(string nameProduct)
        {
            try
            {

                foreach (var pr in db.tbl_products)
                {
                    if (Regex.IsMatch(pr.Name.ToLower(), nameProduct.ToLower()) || pr.Name.ToLower() == nameProduct.ToLower())
                    {
                        return Ok(pr);
                    }

                }
                return BadRequest("The product does not exist");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }
        //[Authorize]
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_products(int id, tbl_products tbl_products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_products.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_products).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_productsExists(id))
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
        //[Authorize]
        // POST: api/Products
        [ResponseType(typeof(tbl_products))]
        public IHttpActionResult Posttbl_products(tbl_products tbl_products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_products.Add(tbl_products);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_products.ID }, tbl_products);
        }
        //[Authorize]
        // DELETE: api/Products/5
        [ResponseType(typeof(tbl_products))]
        public IHttpActionResult Deletetbl_products(int id)
        {
            tbl_products tbl_products = db.tbl_products.Find(id);
            if (tbl_products == null)
            {
                return NotFound();
            }

            db.tbl_products.Remove(tbl_products);
            db.SaveChanges();

            return Ok(tbl_products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_productsExists(int id)
        {
            return db.tbl_products.Count(e => e.ID == id) > 0;
        }
        [HttpGet]
        [Route("api/Products/Search")]
        public IHttpActionResult SearchForProducts(string product_search)
        {
            try
            {
                List<tbl_products> products = new List<tbl_products>();
                foreach (var pr in db.tbl_products)
                {
                    if (Regex.IsMatch(pr.Name.ToLower(), product_search.ToLower()) || pr.Name.ToLower() == product_search.ToLower())
                    {
                        products.Add(pr);
                    }

                }
                if(products.Count != 0)
                {
                    return Ok(products);

                }
                else { 
                return Ok("The product does not exist");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }

    }
}