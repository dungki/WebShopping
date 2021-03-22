using System.Linq;
using WebShopping.Core;
using WebShopping.Core.Objects;

namespace WebShopping.Application.Models
{
    public class Cart
    {
        public string Name { get; set; }
        public int productId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Image { get; set; }
        public decimal Payment { get; set; }

        public Cart(int IproductId)
        {
            using (ShopDbContext db = new ShopDbContext())
            {
                this.productId = IproductId;
                Product product = db.Products.Single(p => p.Id == IproductId);
                this.Name = product.Name;
                this.Image = product.Image;
                this.UnitPrice = product.UnitPrice;
                this.Quantity = 1;
                this.Payment = UnitPrice * Quantity;
            }
        }
        public Cart(int IproductId, int quantity)
        {
            using (ShopDbContext db = new ShopDbContext())
            {
                this.productId = IproductId;
                Product product = db.Products.Single(p => p.Id == IproductId);
                this.Name = product.Name;
                this.Image = product.Image;
                this.UnitPrice = product.UnitPrice;
                this.Quantity = quantity;
                this.Payment = UnitPrice * Quantity;
            }
        }
        public Cart()
        {

        }

    }
}