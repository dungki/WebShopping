using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebShopping.Core.Objects;

namespace WebShopping.Core
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext() : base("name=connstring")
        {
            Database.SetInitializer(new DbInit());
        }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<EnterCoupon> EnterCoupons { get; set; }
        public virtual DbSet<EnterCouponDetails> EnterCouponDetails { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberShipType> MemberShipTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
