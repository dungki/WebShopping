using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopping.Core.Objects;

namespace WebShopping.Core.IRepository
{
    public interface IProductRepository
    {
        List<Product> getProducts();
        Product getProduct(int id);
        Product createProduct(Product product);
        Product updateProduct(Product product);
    }
}
