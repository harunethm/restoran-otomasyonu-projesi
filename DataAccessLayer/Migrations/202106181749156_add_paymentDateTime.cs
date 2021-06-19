namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_paymentDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "DateTime");
        }
    }
}
