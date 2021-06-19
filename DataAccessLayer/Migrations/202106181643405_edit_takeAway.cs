namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_takeAway : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TakeAways", "PaymentMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TakeAways", "PaymentMethod", c => c.Int());
        }
    }
}
