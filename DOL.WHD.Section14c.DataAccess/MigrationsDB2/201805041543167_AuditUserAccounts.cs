namespace DOL.WHD.Section14c.DataAccess.MigrationsDB2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditUserAccounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);


            CreateTable(
                "dbo.UserHistory",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FirstName = c.String(),
                    LastName = c.String(),
                    LastPasswordChangedDate = c.DateTime(nullable: false),
                    Disabled = c.Boolean(nullable: false, defaultValue: false),
                    Deleted = c.Boolean(nullable: false, defaultValue: false),
                    LastModifiedAt = c.DateTime(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    CreatedBy_Id = c.String(maxLength: 128),
                    LastModifiedBy_Id = c.String(maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.LastModifiedBy_Id);


            CreateTable(
                "dbo.UserActivities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        ActionId = c.Int(nullable: false),
                        ActionType = c.String(),
                        HistoryId = c.String(),
                        CreatedBy_Id = c.String(maxLength: 128),
                    CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserActions", t => t.ActionId)
                .Index(t => t.LastModifiedBy_Id);
            
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.UserHistory", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserHistory", "LastModifiedBy_Id", "dbo.Users");
            DropIndex("dbo.UserHistory", new[] { "CreatedBy_Id" });
            DropIndex("dbo.UserHistory", new[] { "LastModifiedBy_Id" });

            DropForeignKey("dbo.UserActivities", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserActivities", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserActivities", "ActionId", "dbo.UserActions");
            DropIndex("dbo.UserActivities", new[] { "LastModifiedBy_Id" });
            DropTable("dbo.UserActivities");
            DropTable("dbo.UserActions");
            DropTable("dbo.UserHistory");
        }
    }
}
