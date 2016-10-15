namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationSaveKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ApplicationSaves");
            AddColumn("dbo.ApplicationSaves", "ApplicationId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ApplicationSaves", "EIN");
            DropColumn("dbo.ApplicationSaves", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationSaves", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.ApplicationSaves");
            DropColumn("dbo.ApplicationSaves", "ApplicationId");
            AddPrimaryKey("dbo.ApplicationSaves", new[] { "UserId", "EIN" });
        }
    }
}
