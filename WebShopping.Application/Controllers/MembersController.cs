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
    public class MembersController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Members
        public ActionResult Index(FormCollection f, int? page)
        {
            if (page > 0 || page % 1 != 0)
            {
                string search = f["search"];
                if (page == null) page = 1;
                var pageSize = 10;
                var pageNumber = (page ?? 1);
                var members = db.Members.OrderByDescending(x => x.Id);
                if (search != null)
                {
                    return View(members.Where(p => p.Name.Contains(search) || p.Phone.Contains(search) || p.Address.Contains(search) || p.Email.Contains(search) || p.Account.Contains(search) || p.MemberShipType.Name.Contains(search)).ToPagedList(pageNumber, pageSize));
                }
                return View(members.ToPagedList(pageNumber, pageSize));
            }
            return View("_ErrorAdmin");
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.MemberShipTypeId = new SelectList(db.MemberShipTypes, "Id", "Name");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Address,Name,Password,Email,Phone,Question,Answer,MemberShipTypeId")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberShipTypeId = new SelectList(db.MemberShipTypes, "Id", "Name", member.MemberShipTypeId);
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberShipTypeId = new SelectList(db.MemberShipTypes, "Id", "Name", member.MemberShipTypeId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,Address,Name,Password,Email,Phone,Question,Answer,MemberShipTypeId")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberShipTypeId = new SelectList(db.MemberShipTypes, "Id", "Name", member.MemberShipTypeId);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
