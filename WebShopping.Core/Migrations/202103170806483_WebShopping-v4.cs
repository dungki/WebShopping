namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Cancel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Cancel");
        }
    }
}
