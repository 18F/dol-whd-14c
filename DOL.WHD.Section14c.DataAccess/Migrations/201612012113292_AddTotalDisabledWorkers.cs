namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalDisabledWorkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployerInfoes", "TotalDisabledWorkers", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployerInfoes", "TotalDisabledWorkers");
        }
    }
}
