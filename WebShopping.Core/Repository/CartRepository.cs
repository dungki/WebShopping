using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopping.Core.IRepository;
using WebShopping.Core.Objects;

namespace WebShopping.Core.Repository
{
    public class CartRepository : ICartRepository
    {
        ShopDbContext db = new ShopDbContext();
        
    }
}
