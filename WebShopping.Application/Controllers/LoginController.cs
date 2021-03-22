using System;
using System.Linq;
using System.Web.Mvc;
using WebShopping.Core;
using WebShopping.Core.IRepository;
using WebShopping.Core.Objects;
using WebShopping.Core.Repository;

namespace WebShopping.Application.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ILoginRepository repository = new LoginRepository();
        private ShopDbContext db = new ShopDbContext();
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Products");
        }
        [HttpPost]
        public ActionResult Login(Member member)
        {
            Member checkMember = repository.GetMember(member.Email, member.Password);

            if(checkMember != null)
            {
                checkMember.Password = null;
                Session["Login"] = checkMember;
                if (checkMember.MemberShipTypeId == 4)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["Status"] = "Login Sucees!";
                    TempData["Message"] = "Go To Shopping";
                    TempData["Style"] = "alert-success";
                    return RedirectToAction("Index");
                }
                
            }
            TempData["Status"] = "Login Fail!";
            TempData["Message"] = "Email or Password is not valid";
            TempData["Style"] = "alert-danger";

            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session["Login"] = null;
            return RedirectToAction("Index","Login");
        }
        public ActionResult Register(int? id)
        {
            if (id.HasValue)
            {
                Member member = db.Members.SingleOrDefault(p => p.Id == id.Value);
                if (member != null)
                {
                    ViewBag.memberId = id;
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");  
        }
        [HttpPost]
        public ActionResult Register(FormCollection f)
        {
            string email = f["Email"];
            Member checkMember = repository.CheckMember(email);
            if (checkMember == null)
            {
                Member member = new Member();
                member.Account = f["Account"];
                member.Email = f["Email"];
                member.Name = f["Name"];
                member.Address = f["Address"];
                member.Password = f["Password"];
                member.Phone = f["Phone"];
                member.Question = f["Question"];
                member.Answer = f["Answer"];
                member.MemberShipTypeId = 3;
                repository.AddMember(member);
                TempData["Status"] = "Register Sucess!";
                TempData["Message"] = "Go To Login.";
                TempData["Style"] = "alert-success";
            }

            TempData["Status"] = "Register Fail!";
            TempData["Message"] = "Email already exists.";
            TempData["Style"] = "alert-danger";
            return RedirectToAction("Index");
        }

    }
}
// sửa lỗi tồn tại email  và phone không thể register