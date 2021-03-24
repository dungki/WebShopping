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
using PagedList;

namespace WebShopping.Application.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Products
        public ActionResult Index(FormCollection f, int? page)
        {
            string search = f["search"];
            if (page == null) page = 1;
            var pageSize = 8;
            var pageNumber = (page ?? 1);
            var products = db.Products.OrderByDescending(p => p.Id);
            if (search != null)
            {
                return View(products.Where(p => p.Name.Contains(search) || p.Supplier.Name.Contains(search) || p.Producer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
            }
            return View(products.ToPagedList(pageNumber, pageSize));
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
