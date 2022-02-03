using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiFlowerTwo.Controllers
{
    public class ProductsFavoritesController : Controller
    {
        // GET: ProductsFavorites
        public ActionResult Index()
        {
            return View();
        }
    }
}