namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrganizationMembership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationMemberships",
                c => new
                    {
                        MembershipId = c.Guid(nullable: false),
                        EIN = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationMemberships", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OrganizationMemberships", new[] { "ApplicationUser_Id" });
            DropTable("dbo.OrganizationMemberships");
        }
    }
}
