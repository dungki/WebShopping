using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string NickName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
