namespace DOL.WHD.Section14c.DataAccess.MigrationsDB2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificates",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    CertificateNumber = c.String(),
                    PriviteId = c.String(),
                    CreatedBy_Id = c.String(),
                    CreatedAt = c.DateTime(nullable: false),
                    LastModifiedBy_Id = c.String(maxLength: 128),
                    LastModifiedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);

            CreateTable(
                "dbo.Employers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CertificateNumber_Id = c.String(maxLength: 128),
                        LegalName = c.String(),
                        EIN = c.String(),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        PhysicalAddress_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Certificates", t => t.CertificateNumber_Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Addresses", t => t.PhysicalAddress_Id)
                .Index(t => t.CertificateNumber_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PhysicalAddress_Id);
            
            AddColumn("dbo.ApplicationSaves", "ApplicationId", c => c.String());
            AddColumn("dbo.ApplicationSaves", "Employer_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserClaims", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserClaims", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserLogins", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserLogins", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrganizationMemberships", "ApplicationId", c => c.String());
            AddColumn("dbo.OrganizationMemberships", "Employer_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserRoles", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserRoles", "CreatedAt", c => c.DateTime(nullable: false));
            CreateIndex("dbo.ApplicationSaves", "Employer_Id");
            CreateIndex("dbo.OrganizationMemberships", "Employer_Id");
            AddForeignKey("dbo.OrganizationMemberships", "Employer_Id", "dbo.Employers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationSaves", "Employer_Id", "dbo.Employers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSaves", "Employer_Id", "dbo.Employers");
            DropForeignKey("dbo.OrganizationMemberships", "Employer_Id", "dbo.Employers");
            DropForeignKey("dbo.Certificates", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Employers", "CertificateNumber_Id", "dbo.Certificates");
            DropForeignKey("dbo.Employers", "PhysicalAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Employers", "LastModifiedBy_Id", "dbo.Users");
            DropIndex("dbo.Certificates", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Employers", new[] { "CertificateNumber_Id" });
            DropIndex("dbo.Employers", new[] { "PhysicalAddress_Id" });
            DropIndex("dbo.Employers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.OrganizationMemberships", new[] { "Employer_Id" });
            DropIndex("dbo.ApplicationSaves", new[] { "Employer_Id" });
            DropColumn("dbo.UserRoles", "CreatedAt");
            DropColumn("dbo.UserRoles", "LastModifiedAt");
            DropColumn("dbo.OrganizationMemberships", "Employer_Id");
            DropColumn("dbo.OrganizationMemberships", "ApplicationId");
            DropColumn("dbo.UserLogins", "CreatedAt");
            DropColumn("dbo.UserLogins", "LastModifiedAt");
            DropColumn("dbo.UserClaims", "CreatedAt");
            DropColumn("dbo.UserClaims", "LastModifiedAt");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Users", "LastModifiedAt");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.ApplicationSaves", "Employer_Id");
            DropColumn("dbo.ApplicationSaves", "ApplicationId");
            DropTable("dbo.Employers");
            DropTable("dbo.Certificates");
        }
    }
}
