using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Core;
using WebShopping.Core.Objects;

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
        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Products");
            }
            Member member = db.Members.Find(id);
            return View(member);
        }
        public ActionResult UpdateProfile(FormCollection f)
        {
            int id = Int32.Parse(f["Id"]);
            Member member = db.Members.Single(p => p.Id == id);
            if (!String.IsNullOrEmpty(f["OldPassword"]))
            {
                string oldPass = f["OldPassword"];
                if (member.Password != oldPass)
                {
                    TempData["Mess"] = "Old Password Is Incorrect.";
                    TempData["Icon"] = "fa-info-circle";
                    TempData["Style"] = "red";
                    return RedirectToAction("Profile",new { id = id });
                }
            }
            if (!String.IsNullOrEmpty(f["NewPassword"]))
            {
                string newPass = f["NewPassword"];
                member.Password = newPass;
            }
            db.SaveChanges();
            TempData["Icon"] = "fa-check-circle";
            TempData["Mess"] = "Password Changed Successfully";
            TempData["Style"] = "green";
            return RedirectToAction("Profile",new { id = id });
        }
    }
}