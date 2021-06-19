namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_paymentClass2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payments", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "Discount", c => c.Double(nullable: false));
        }
    }
}
