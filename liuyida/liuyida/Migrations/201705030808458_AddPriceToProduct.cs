namespace liuyida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Price");
        }
    }
}
