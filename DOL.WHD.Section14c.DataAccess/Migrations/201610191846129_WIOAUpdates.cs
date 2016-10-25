namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WIOAUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WIOAs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HasVerfiedDocumentaion = c.Boolean(nullable: false),
                        HasWIOAWorkers = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WIOAWorkers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        WIOAWorkerVerified_Id = c.Int(nullable: false),
                        WIOA_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Responses", t => t.WIOAWorkerVerified_Id, cascadeDelete: true)
                .ForeignKey("dbo.WIOAs", t => t.WIOA_Id)
                .Index(t => t.WIOAWorkerVerified_Id)
                .Index(t => t.WIOA_Id);
            
            AddColumn("dbo.ApplicationSubmissions", "WIOA_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ApplicationSubmissions", "WIOA_Id");
            AddForeignKey("dbo.ApplicationSubmissions", "WIOA_Id", "dbo.WIOAs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSubmissions", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.WIOAWorkers", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.WIOAWorkers", "WIOAWorkerVerified_Id", "dbo.Responses");
            DropIndex("dbo.WIOAWorkers", new[] { "WIOA_Id" });
            DropIndex("dbo.WIOAWorkers", new[] { "WIOAWorkerVerified_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "WIOA_Id" });
            DropColumn("dbo.ApplicationSubmissions", "WIOA_Id");
            DropTable("dbo.WIOAWorkers");
            DropTable("dbo.WIOAs");
        }
    }
}
