namespace WebShopping.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShoppingv3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Password", c => c.String());
        }

    }
}
