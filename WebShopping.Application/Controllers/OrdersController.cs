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
    public class OrdersController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Orders
        public ActionResult Index(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                string search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var orders = db.Orders.Where(p => p.Deleted == false && p.Status == 0).OrderByDescending(x => x.Id);
                if (search != null)
                {
                    return View(orders.Where(p => p.Customer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View(orders.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,Status,DeliveryDate,Payment,Cancel,Deleted,Endow,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,Status,DeliveryDate,Payment,Cancel,Deleted,Endow,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult OrderSuccess(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                ViewBag.title = "OrderSuccess";
                var search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var orders = db.Orders.Where(p => p.Status == 1 && p.Deleted == false).OrderByDescending(p => p.Id);
                if (search != null)
                {
                    return View("list", orders.Where(p => p.Customer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                } 
                return View("List",orders.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
            
        }
        public ActionResult OrderFalse(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                ViewBag.title = "OrderFalse";
                var search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var orders = db.Orders.Where(p => p.Status == 2 && p.Deleted == false).OrderByDescending(p => p.Id);
                if (search != null)
                {
                    return View("list", orders.Where(p => p.Customer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View("List", orders.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
        }
        public ActionResult DeliverySuccess(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                ViewBag.title = "DeliverySuccess";
                var search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var orders = db.Orders.Where(p => p.Status == 1 && p.Payment == true && p.Deleted == false).OrderByDescending(p => p.Id);
                if (search != null)
                {
                    return View("list", orders.Where(p => p.Customer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View("List", orders.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
        }
        public ActionResult DeliveryFalse(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                ViewBag.title = "DeliveryFalse";
                var search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var orders = db.Orders.Where(p => p.Status == 1 && p.Payment == false && p.Deleted == false).OrderByDescending(p => p.Id);
                if (search != null)
                {
                    return View("list", orders.Where(p => p.Customer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View("List", orders.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
        }

        public ActionResult DeletedOrder(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                ViewBag.title = "DeletedOrder";
                var search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var orders = db.Orders.Where(p => p.Deleted == true).OrderByDescending(p => p.Id);
                if (search != null)
                {
                    return View("list", orders.Where(p => p.Customer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View("List", orders.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
        }
        public ActionResult CancelOrder(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                ViewBag.title = "DeletedOrder";
                var search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var orders = db.Orders.Where(p => p.Cancel==true).OrderByDescending(p => p.Id);
                if (search != null)
                {
                    return View("list", orders.Where(p => p.Customer.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View("List", orders.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
        }

    }
}
