namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSCACountField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployerInfoes", "SCACount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployerInfoes", "SCACount");
        }
    }
}
