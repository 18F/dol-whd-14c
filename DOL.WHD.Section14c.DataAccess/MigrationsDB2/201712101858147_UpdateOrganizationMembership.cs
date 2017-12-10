namespace DOL.WHD.Section14c.DataAccess.MigrationsDB2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganizationMembership : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationMemberships", "ApplicationStatusId", c => c.Int());
            CreateIndex("dbo.OrganizationMemberships", "ApplicationStatusId");
            AddForeignKey("dbo.OrganizationMemberships", "ApplicationStatusId", "dbo.Status", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationMemberships", "ApplicationStatusId", "dbo.Status");
            DropIndex("dbo.OrganizationMemberships", new[] { "ApplicationStatusId" });
            DropColumn("dbo.OrganizationMemberships", "ApplicationStatusId");
        }
    }
}
