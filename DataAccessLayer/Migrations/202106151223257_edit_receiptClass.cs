namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_receiptClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receipts", "Paid", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receipts", "Paid");
        }
    }
}
