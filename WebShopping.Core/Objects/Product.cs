using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; } //don gia
        public DateTime UpdateDay { get; set; }
        public string Configuration { get; set; } //cau hinh
        public string Description { get; set; }
        public string Image { get; set; }
        public int QuantityInStock { get; set; } //so luong ton
        public bool Deleted { get; set; }
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public virtual Producer Producer { get; set; }
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<EnterCouponDetails> EnterCouponDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
