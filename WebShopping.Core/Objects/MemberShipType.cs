using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class MemberShipType //loai thanh vien
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Endow { get; set; } //uu dai
        public virtual ICollection<Member> Members { get; set; }
    }
}
