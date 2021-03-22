using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Core;

namespace WebShopping.Application.Controllers
{
    public class HomeController : Controller
    {
        private ShopDbContext db = new ShopDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Customer()
        {
            var cus = db.Customers.ToList();
            return View(cus);
        }
    }
}