using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class EnterCouponDetails //chi tiet phieu nhap
    {
        public int Id { get; set; }
        public int UnitPriceEntry { get; set; } //don gia nhap
        public int NumberOfImport { get; set; } //so luong nhap
        public int EnterCouponId { get; set; }
        [ForeignKey("EnterCouponId")]
        public virtual EnterCoupon EnterCoupon { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
