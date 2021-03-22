namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Status", c => c.Boolean(nullable: false));
        }
    }
}
