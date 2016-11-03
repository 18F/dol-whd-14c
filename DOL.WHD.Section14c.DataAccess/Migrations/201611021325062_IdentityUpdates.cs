namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityUpdates : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "UserClaims");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "UserLogins");
            RenameTable(name: "dbo.AspNetRoles", newName: "Roles");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRoles");
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
                .ForeignKey("dbo.Roles", t => t.ApplicationRole_Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
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
            
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleFeatures", "Feature_Id", "dbo.Features");
            DropForeignKey("dbo.RoleFeatures", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.RoleFeatures", "ApplicationRole_Id", "dbo.Roles");
            DropIndex("dbo.RoleFeatures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.RoleFeatures", new[] { "Feature_Id" });
            DropIndex("dbo.RoleFeatures", new[] { "ApplicationRole_Id" });
            DropTable("dbo.Features");
            DropTable("dbo.RoleFeatures");
            RenameTable(name: "dbo.UserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Roles", newName: "AspNetRoles");
            RenameTable(name: "dbo.UserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.UserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
        }
    }
}
