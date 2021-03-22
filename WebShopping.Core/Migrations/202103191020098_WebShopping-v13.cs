namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv13 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "NewProduct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "NewProduct", c => c.Int(nullable: false));
        }
    }
}
