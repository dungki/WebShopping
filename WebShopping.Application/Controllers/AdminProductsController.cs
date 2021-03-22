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
using System.IO;

namespace WebShopping.Application.Controllers
{
    public class AdminProductsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: AminProducts
        public ActionResult Index(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                string search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var products = db.Products.OrderByDescending(x => x.Id);
                if (search != null)
                {
                    return View(products.Where(p => p.Name.Contains(search) || p.Configuration.Contains(search) || p.UnitPrice.ToString().Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View(products.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
            
        }

        // GET: AminProducts/Details/5
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

        // GET: AminProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name");
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        // POST: AminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/products"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = "Image already exists.";
                    }
                    else
                    {
                        Image.SaveAs(path);
                        product.Image = fileName;
                    }
                }
                product.UpdateDay = DateTime.Now;
                product.Deleted = false;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", product.ProducerId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: AminProducts/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", product.ProducerId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // POST: AminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", product.ProducerId);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: AminProducts/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: AminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
    }
}
