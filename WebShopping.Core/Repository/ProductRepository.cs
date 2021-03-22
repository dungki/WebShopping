using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopping.Core.IRepository;
using WebShopping.Core.Objects;

namespace WebShopping.Core.Repository
{
    public class ProductRepository : IProductRepository
    {
        ShopDbContext db = new ShopDbContext();
        public Product createProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product getProduct(int id)
        {
            return db.Products.SingleOrDefault(x => x.Id == id);
        }

        public List<Product> getProducts()
        {
            return db.Products.OrderByDescending(p=>p.Id).ToList();
        }

        public Product updateProduct(Product product)
        {
            Product pro = db.Products.SingleOrDefault(x => x.Id == product.Id);
            pro.Name = product.Name;
            pro.UnitPrice = product.UnitPrice;
            pro.UpdateDay = product.UpdateDay;
            pro.Configuration = product.Configuration;
            pro.Description = product.Description;
            pro.Image = product.Image;
            pro.QuantityInStock = product.QuantityInStock;
            pro.Deleted = product.Deleted;
            pro.SupplierId = product.SupplierId;
            pro.ProducerId = product.ProducerId;
            pro.ProductTypeId = product.ProductTypeId;
            db.Products.Add(pro);
            db.SaveChanges();
            return pro;

        }
    }
}
