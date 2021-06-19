namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_paymentClass1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "Receipt_ReceiptID", "dbo.Receipts");
            DropIndex("dbo.Payments", new[] { "Receipt_ReceiptID" });
            DropColumn("dbo.Payments", "Receipt_ReceiptID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "Receipt_ReceiptID", c => c.Int());
            CreateIndex("dbo.Payments", "Receipt_ReceiptID");
            AddForeignKey("dbo.Payments", "Receipt_ReceiptID", "dbo.Receipts", "ReceiptID");
        }
    }
}
