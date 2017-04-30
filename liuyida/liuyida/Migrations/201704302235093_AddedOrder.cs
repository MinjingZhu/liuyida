namespace liuyida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeliveryTime = c.DateTime(nullable: false),
                        DeliveryFee = c.Double(nullable: false),
                        DeliveryMethod = c.Int(nullable: false),
                        DeliveryAddress = c.String(),
                        PaymentMethod = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Paid = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.Orders");
        }
    }
}
