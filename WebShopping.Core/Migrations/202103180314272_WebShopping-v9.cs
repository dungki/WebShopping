namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Address", c => c.String());
            AddColumn("dbo.Members", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Name");
            DropColumn("dbo.Members", "Address");
        }
    }
}
