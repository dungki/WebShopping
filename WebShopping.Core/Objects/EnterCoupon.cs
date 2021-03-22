using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class EnterCoupon //phieu nhap
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<EnterCouponDetails> EnterCouponDetails { get; set; }
    }
}
