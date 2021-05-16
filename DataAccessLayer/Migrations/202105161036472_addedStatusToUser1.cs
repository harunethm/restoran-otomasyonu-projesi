namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStatusToUser1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receipts", "ReceiptDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TakeAways", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TakeAways", "Status");
            DropColumn("dbo.Receipts", "ReceiptDate");
        }
    }
}
