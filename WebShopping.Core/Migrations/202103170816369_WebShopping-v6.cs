namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.OrderDetails", "Endow");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Endow", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "UnitPrice");
        }
    }
}
