namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkSiteTypeSingleSelection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSiteTypeId", "dbo.Responses");
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSiteId" });
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSiteTypeId" });
            AddColumn("dbo.WorkSites", "WorkSiteTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkSites", "WorkSiteTypeId");
            DropTable("dbo.WorkSiteWorkSiteType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WorkSiteWorkSiteType",
                c => new
                    {
                        WorkSiteId = c.Guid(nullable: false),
                        WorkSiteTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkSiteId, t.WorkSiteTypeId });
            
            DropIndex("dbo.WorkSites", new[] { "WorkSiteTypeId" });
            DropColumn("dbo.WorkSites", "WorkSiteTypeId");
            CreateIndex("dbo.WorkSiteWorkSiteType", "WorkSiteTypeId");
            CreateIndex("dbo.WorkSiteWorkSiteType", "WorkSiteId");
            AddForeignKey("dbo.WorkSiteWorkSiteType", "WorkSiteTypeId", "dbo.Responses", "Id", cascadeDelete: true);
        }
    }
}
