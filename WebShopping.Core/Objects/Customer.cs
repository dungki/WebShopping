using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopping.Core.Objects
{
    public class Customer //khach hang
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="*Please enter a name.")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
