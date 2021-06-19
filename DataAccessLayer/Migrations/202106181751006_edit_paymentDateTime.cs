namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_paymentDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PaymentDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Payments", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Payments", "PaymentDateTime");
        }
    }
}
