using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } //ngay dat hang
        public int Status { get; set; }
        public DateTime DeliveryDate { get; set; } //ngay giao hang
        public bool Payment { get; set; } //thanh toan
        public bool Cancel { get; set; }
        public bool Deleted { get; set; }
        public int Endow { get; set; } //uu dai
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
