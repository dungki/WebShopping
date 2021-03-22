using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class Supplier //nha cung cap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public virtual ICollection<EnterCoupon> EnterCoupons { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
