namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "View");
            DropColumn("dbo.Products", "Vote");
            DropColumn("dbo.Products", "NumberOfTimesPurchased");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "NumberOfTimesPurchased", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Vote", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "View", c => c.Int(nullable: false));
        }
    }
}
