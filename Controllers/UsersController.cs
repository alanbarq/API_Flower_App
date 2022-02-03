using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiFlowerTwo.Models;

namespace WebApiFlowerTwo.Controllers
{
    [AllowAnonymous]
    public class UsersController : ApiController
    {
        private FlowerAppEntities db = new FlowerAppEntities();

        // GET: api/Users
        public List<tbl_users> Gettbl_users()
        {
            return db.tbl_users.ToList();
        }

        // GET: api/Users/5
        [ResponseType(typeof(tbl_users))]
        public IHttpActionResult Gettbl_users(int id)
        {
            tbl_users tbl_users = db.tbl_users.Find(id);
            if (tbl_users == null)
            {
                return NotFound();
            }

            return Ok(tbl_users);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_users(int id, tbl_users tbl_users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_users.ID_user)
            {
                return BadRequest();
            }

            db.Entry(tbl_users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_usersExists(id))
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
        
 
        
        [ResponseType(typeof(tbl_users))]
        [Route("api/Users/addNewUser")]
        public IHttpActionResult Posttbl_users(tbl_users tbl_users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_users.Add(tbl_users);
            db.SaveChanges();

            return Ok(tbl_users);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(tbl_users))]
        public IHttpActionResult Deletetbl_users(int id)
        {
            tbl_users tbl_users = db.tbl_users.Find(id);
            if (tbl_users == null)
            {
                return NotFound();
            }

            db.tbl_users.Remove(tbl_users);
            db.SaveChanges();

            return Ok(tbl_users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_usersExists(int id)
        {
            return db.tbl_users.Count(e => e.ID_user == id) > 0;
        }
        
        [HttpPost]
        [ResponseType(typeof(tbl_users))]
        public IHttpActionResult PostLoginTwo([FromBody] Login thelogin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var val_User = db.tbl_users.Where(x => x.username == thelogin.Username && x.password == thelogin.Password).FirstOrDefault();
                if (val_User != null)
                {
                    if(val_User.Role == 2)
                    {
                        var jwkToken = TokenGenerator.GenerateTokenJwt(val_User.username, "Administrator");
                        var token = new JWT_jsonModel(val_User.username, jwkToken, "Administrator",val_User.ID_user);
                        return Ok(token);
                    }
                    else if(val_User.Role == 1)
                    {
                        var jwkToken = TokenGenerator.GenerateTokenJwt(val_User.username, "User");
                        var token = new JWT_jsonModel(val_User.username, jwkToken, "User",val_User.ID_user);
                        return Ok(token);
                    }
                    else
                    {
                        var token = new JWT_jsonModel("", "", "Unauthorized",0);
                        return Ok(token);
                    }
                }
                else
                {
                    var token = new JWT_jsonModel("", "", "Unauthorized",0);
                    return Ok(token);
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.StackTrace);
                return BadRequest("There is a problem retrieving the data");
            }
        }
    }
}