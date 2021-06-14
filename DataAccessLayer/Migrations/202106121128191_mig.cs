namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Tables", "Availability", c => c.Int(nullable: false));
            //DropColumn("dbo.Tables", "Status");
            RenameColumn("dbo.Tables", "Status", "Availability");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Tables", "Status", c => c.Int(nullable: false));
            //DropColumn("dbo.Tables", "Availability");
            RenameColumn("dbo.Tables", "Availability", "Status");
        }
    }
}
