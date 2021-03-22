using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Application.Models;
using WebShopping.Core;
using WebShopping.Core.IRepository;
using WebShopping.Core.Objects;
using WebShopping.Core.Repository;

namespace WebShopping.Application.Controllers
{
    public class CartController : Controller
    {
        ILoginRepository repository = new LoginRepository();

        ShopDbContext db = new ShopDbContext();
        // GET: Cart
        public List<Cart> getCart()
        {
            //giỏ hàng đã tồn tại
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if(carts == null)
            {
                //session null thì tạo giỏ hàng
                carts = new List<Cart>();
                Session["Cart"] = carts;
            }
            return carts;
        }

        public ActionResult CreateCart(int id, string url)
        {
            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<Cart> listCart = getCart();

            //nếu sản phẩm đã tồn tai trong giỏ 
            Cart check = listCart.SingleOrDefault(p => p.productId == id);
            if (check != null)
            {
                //kiểm tra số lượng tồn
                if (product.QuantityInStock < check.Quantity)
                {
                    return View("ErrorProduct");
                }
                check.Quantity++;
                check.Payment = check.Quantity * check.UnitPrice;
                return Redirect(url);
            }
            Cart itemCart = new Cart(id);
            if (product.QuantityInStock < itemCart.Quantity)
            {
                return View("ErrorProduct");
            }
            listCart.Add(itemCart);
            return Redirect(url);
        }
        public double TotalQuantity()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                return 0;
            }
            return listCart.Sum(p => p.Quantity);
        }
        public decimal TotalPayment()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                return 0;
            }
            return listCart.Sum(p => p.Payment);
        }
        public ActionResult CartIcon()
        {
            if (TotalQuantity() == 0)
            {
                ViewBag.TotalQuantity = 0;
                ViewBag.TotalPayment = 0;
                return PartialView();
            }
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.TotalPayment = TotalPayment();

            return PartialView();
        }
        public ActionResult ViewCart()
        {
            List<Cart> listCart = getCart();
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.TotalPayment = TotalPayment();
            return View(listCart);
        }
        [HttpGet]
        public ActionResult EditCart(int id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Product");
            }
            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = getCart();
            Cart checkProduct = listCart.SingleOrDefault(p => p.productId == id);
            if (checkProduct == null)
            {
                return RedirectToAction("Index", "Product");
            }
            ViewBag.Cart = listCart;
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.TotalPayment = TotalPayment();
            return View(checkProduct);
        }
        [HttpPost]
        public ActionResult UpdateCart(Cart cart)
        {
            Product checkProduct = db.Products.Single(p => p.Id == cart.productId);
            if (checkProduct.QuantityInStock < cart.Quantity)
            {
                return View("ErrorProduct");
            }
            List<Cart> listCart = getCart();
            Cart itemCartUpdate = listCart.Find(p => p.productId == cart.productId);
            itemCartUpdate.Quantity = cart.Quantity;
            itemCartUpdate.Payment = itemCartUpdate.Quantity * itemCartUpdate.UnitPrice;
            return RedirectToAction("ViewCart");
        }
        public ActionResult DeleteCart(int id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Product");
            }
            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = getCart();
            Cart checkProduct = listCart.SingleOrDefault(p => p.productId == id);
            if (checkProduct == null)
            {
                return RedirectToAction("Index", "Product");
            }
            listCart.Remove(checkProduct);

            return RedirectToAction("ViewCart");
        }
        public ActionResult Payment(FormCollection f, Customer cus)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Product");
            }

            Customer customer = new Customer();
            if (Session["Login"] == null)
            {
                string email = f["Email"];
                Member checkMember = repository.CheckMember(email);
                if (checkMember == null)
                {
                    customer = cus;
                }

                customer.MemberId = checkMember.Id;
                customer.Phone = checkMember.Phone;
                customer.Name = checkMember.Name;
                customer.Address = checkMember.Address;
                customer.Email = checkMember.Email;

                db.Customers.Add(customer);
                db.SaveChanges();
            }
            else
            {
                Member member = Session["Login"] as Member;
                customer.Name = member.Name;
                customer.Address = member.Address;
                customer.Email = member.Email;
                customer.Phone = member.Phone;
                customer.MemberId = member.Id;
                db.Customers.Add(customer);
                db.SaveChanges();
            }


            Order order = new Order();
            order.CustomerId = customer.Id;
            order.OrderDate = DateTime.Now;
            order.DeliveryDate = DateTime.Now;
            order.Status = 0;
            order.Payment = false;
            order.Endow = 0;
            order.Cancel = false;
            order.Deleted = false;              //customerId đang để null

            db.Orders.Add(order);
            db.SaveChanges();
            List<Cart> listCart = getCart();
            foreach (var item in listCart)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderId = order.Id;
                orderDetail.ProductId = item.productId;
                orderDetail.Name = item.Name;
                orderDetail.Quantity = item.Quantity;
                orderDetail.UnitPrice = item.UnitPrice;
                db.OrderDetails.Add(orderDetail);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            TempData["Status"] = "Payment Sucees!";
            TempData["Message"] = "Go To Shopping";
            TempData["Style"] = "alert-success";
            return RedirectToAction("Index","Products");
        }

    }
}