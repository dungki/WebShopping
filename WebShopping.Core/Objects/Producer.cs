using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class Producer //nha san xuat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string Logo { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
