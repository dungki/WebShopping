using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Core;
using WebShopping.Core.IRepository;
using WebShopping.Core.Repository;
using PagedList;
using WebShopping.Core.Objects;

namespace WebShopping.Application.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ShopDbContext db = new ShopDbContext();
        public ActionResult Index()
        {
            ViewBag.paySuccess = TotalPaymentMoneySuccess();
            ViewBag.payProduct = TotalProductPaymentMoney();
            ViewBag.order = TotalOrderProseccing();
            ViewBag.totalSuccess = TotalOrderSuccess();
            ViewBag.orderFalse = TotalOrderFalse();
            return View();
        }
        public decimal TotalPaymentMoneySuccess()
        {
            var paySuccess = db.Orders.Where(p => p.Payment == true && p.Status == 1).ToList();
            decimal total = 0;
            foreach (var item in paySuccess)
            {
                total += decimal.Parse(item.OrderDetails.Sum(p => p.Quantity * p.UnitPrice).ToString());
            }

            return total;
        }
        public decimal TotalProductPaymentMoney()
        {
            decimal total = decimal.Parse(db.Products.Sum(p => p.UnitPrice * p.QuantityInStock).ToString());
            return total;
        }
        public int TotalOrderProseccing()
        {
            var orderSuccess = db.Orders.Where(p => p.Status == 0).ToList();
            return orderSuccess.Count();
        }
        public int TotalOrderSuccess()
        {
            var orderSuccess = db.Orders.Where(p => p.Status == 1).ToList();
            return orderSuccess.Count();
        }
        public int TotalOrderFalse()
        {
            var orderFalse= db.Orders.Where(p => p.Status == 2).ToList();
            return orderFalse.Count();
        }
    }
}