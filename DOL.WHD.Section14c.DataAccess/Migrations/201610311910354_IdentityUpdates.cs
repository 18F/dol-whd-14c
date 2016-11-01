namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleFeatures",
                c => new
                    {
                        RoleFeatureId = c.Int(nullable: false, identity: true),
                        ApplicationRole_Id = c.String(maxLength: 128),
                        Feature_Id = c.Int(nullable: false),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RoleFeatureId)
                .ForeignKey("dbo.AspNetRoles", t => t.ApplicationRole_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Features", t => t.Feature_Id, cascadeDelete: true)
                .Index(t => t.ApplicationRole_Id)
                .Index(t => t.Feature_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RoleFeatures", "Feature_Id", "dbo.Features");
            DropForeignKey("dbo.RoleFeatures", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RoleFeatures", "ApplicationRole_Id", "dbo.AspNetRoles");
            DropIndex("dbo.RoleFeatures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.RoleFeatures", new[] { "Feature_Id" });
            DropIndex("dbo.RoleFeatures", new[] { "ApplicationRole_Id" });
            DropTable("dbo.Features");
            DropTable("dbo.RoleFeatures");
        }
    }
}
