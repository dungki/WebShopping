using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class Member //thanh vien
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int MemberShipTypeId { get; set; }
        [ForeignKey("MemberShipTypeId")]
        public virtual MemberShipType MemberShipType { get; set; }
    }
}
