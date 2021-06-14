namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "Name", c => c.String());
            AddColumn("dbo.Tables", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "Status");
            DropColumn("dbo.Tables", "Name");
        }
    }
}
