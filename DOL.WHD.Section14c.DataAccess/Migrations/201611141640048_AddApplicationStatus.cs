namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ApplicationSubmissions", "StatusId", c => c.Int(nullable: false, defaultValue: 1)); // default to pending
            CreateIndex("dbo.ApplicationSubmissions", "StatusId");
            AddForeignKey("dbo.ApplicationSubmissions", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSubmissions", "StatusId", "dbo.Status");
            DropIndex("dbo.ApplicationSubmissions", new[] { "StatusId" });
            DropColumn("dbo.ApplicationSubmissions", "StatusId");
            DropTable("dbo.Status");
        }
    }
}
