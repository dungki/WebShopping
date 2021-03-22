using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopping.Core;
using WebShopping.Core.Objects;

namespace WebShopping.Application.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Products
        public ActionResult Index(FormCollection f)
        {
            string search = f["search"];
            var products = db.Products.OrderByDescending(p => p.Id);
            int i = 1;
            ViewBag.count = i++;
            if (search != null)
            {
                return View(products.Where(p => p.Name.Contains(search)).ToList());
            }
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

       
    }
}
