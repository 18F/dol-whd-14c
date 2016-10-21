namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicationId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationSaves", "ApplicationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationSaves", "ApplicationId", c => c.Guid(nullable: false));
        }
    }
}
