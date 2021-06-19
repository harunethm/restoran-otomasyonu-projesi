namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_PaymentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        PaymentMethod = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Receipt_ReceiptID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Receipts", t => t.Receipt_ReceiptID)
                .Index(t => t.OrderID)
                .Index(t => t.Receipt_ReceiptID);
            
            DropColumn("dbo.Receipts", "PaymentMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Receipts", "PaymentMethod", c => c.Int(nullable: false));
            DropForeignKey("dbo.Payments", "Receipt_ReceiptID", "dbo.Receipts");
            DropForeignKey("dbo.Payments", "OrderID", "dbo.Orders");
            DropIndex("dbo.Payments", new[] { "Receipt_ReceiptID" });
            DropIndex("dbo.Payments", new[] { "OrderID" });
            DropTable("dbo.Payments");
        }
    }
}
