namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployerUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployerInfoes", "HasDifferentMailingAddress", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployerInfoes", "HasDifferentMailingAddress");
        }
    }
}
