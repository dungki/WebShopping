namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Deleted");
        }
    }
}
