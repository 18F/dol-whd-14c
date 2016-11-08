namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveHasDifferentMailingAddress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EmployerInfoes", "HasDifferentMailingAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployerInfoes", "HasDifferentMailingAddress", c => c.Boolean(nullable: false));
        }
    }
}
