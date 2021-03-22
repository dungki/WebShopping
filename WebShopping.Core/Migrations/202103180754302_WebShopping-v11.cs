namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "MemberId", "dbo.Members");
            DropIndex("dbo.Customers", new[] { "MemberId" });
            AlterColumn("dbo.Customers", "MemberId", c => c.Int());
            CreateIndex("dbo.Customers", "MemberId");
            AddForeignKey("dbo.Customers", "MemberId", "dbo.Members", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "MemberId", "dbo.Members");
            DropIndex("dbo.Customers", new[] { "MemberId" });
            AlterColumn("dbo.Customers", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "MemberId");
            AddForeignKey("dbo.Customers", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
        }
    }
}
