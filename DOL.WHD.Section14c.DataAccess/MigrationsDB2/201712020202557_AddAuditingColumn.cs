namespace DOL.WHD.Section14c.DataAccess.MigrationsDB2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditingColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreatedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserClaims", "CreatedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserClaims", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserLogins", "CreatedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserLogins", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserRoles", "CreatedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserRoles", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Roles", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Roles", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Roles", "CreatedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Roles", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "CreatedBy_Id");
            CreateIndex("dbo.Users", "LastModifiedBy_Id");
            CreateIndex("dbo.UserClaims", "CreatedBy_Id");
            CreateIndex("dbo.UserClaims", "LastModifiedBy_Id");
            CreateIndex("dbo.UserLogins", "CreatedBy_Id");
            CreateIndex("dbo.UserLogins", "LastModifiedBy_Id");
            CreateIndex("dbo.UserRoles", "CreatedBy_Id");
            CreateIndex("dbo.UserRoles", "LastModifiedBy_Id");
            CreateIndex("dbo.Roles", "CreatedBy_Id");
            CreateIndex("dbo.Roles", "LastModifiedBy_Id");
            AddForeignKey("dbo.UserClaims", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserClaims", "LastModifiedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "LastModifiedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserLogins", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserLogins", "LastModifiedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRoles", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRoles", "LastModifiedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Roles", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Roles", "LastModifiedBy_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Roles", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "CreatedBy_Id", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Roles", new[] { "CreatedBy_Id" });
            DropIndex("dbo.UserRoles", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.UserRoles", new[] { "CreatedBy_Id" });
            DropIndex("dbo.UserLogins", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.UserLogins", new[] { "CreatedBy_Id" });
            DropIndex("dbo.UserClaims", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.UserClaims", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Users", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Users", new[] { "CreatedBy_Id" });
            DropColumn("dbo.Roles", "LastModifiedBy_Id");
            DropColumn("dbo.Roles", "CreatedBy_Id");
            DropColumn("dbo.Roles", "CreatedAt");
            DropColumn("dbo.Roles", "LastModifiedAt");
            DropColumn("dbo.UserRoles", "LastModifiedBy_Id");
            DropColumn("dbo.UserRoles", "CreatedBy_Id");
            DropColumn("dbo.UserLogins", "LastModifiedBy_Id");
            DropColumn("dbo.UserLogins", "CreatedBy_Id");
            DropColumn("dbo.UserClaims", "LastModifiedBy_Id");
            DropColumn("dbo.UserClaims", "CreatedBy_Id");
            DropColumn("dbo.Users", "LastModifiedBy_Id");
            DropColumn("dbo.Users", "CreatedBy_Id");
        }
    }
}
