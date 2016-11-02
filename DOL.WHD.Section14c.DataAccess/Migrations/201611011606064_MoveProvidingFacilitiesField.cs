namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveProvidingFacilitiesField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmissionId", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses");
            DropIndex("dbo.EmployerInfoes", new[] { "ProvidingFacilitiesDeductionTypeId" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmissionId" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ProvidingFacilitiesDeductionTypeId" });
            CreateTable(
                "dbo.EmployerInfoFacilitiesDeductionType",
                c => new
                    {
                        EmployerInfoId = c.Guid(nullable: false),
                        ProvidingFacilitiesDeductionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployerInfoId, t.ProvidingFacilitiesDeductionTypeId })
                .ForeignKey("dbo.Responses", t => t.ProvidingFacilitiesDeductionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.EmployerInfoes", t => t.EmployerInfoId, cascadeDelete: true)
                .Index(t => t.EmployerInfoId)
                .Index(t => t.ProvidingFacilitiesDeductionTypeId);
            
            DropColumn("dbo.ApplicationSubmissions", "RepresentativePayeeSocialSecurityBenefits");
            DropColumn("dbo.ApplicationSubmissions", "NumEmployeesRepresentativePayee");
            DropColumn("dbo.ApplicationSubmissions", "ProvidingFacilities");
            DropColumn("dbo.ApplicationSubmissions", "ProvidingFacilitiesDeductionTypeOther");
            DropColumn("dbo.ApplicationSubmissions", "ReviewedDocumentation");
            DropColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeId");
            DropTable("dbo.AppSubmissionFacilitiesDeductionType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppSubmissionFacilitiesDeductionType",
                c => new
                    {
                        ApplicationSubmissionId = c.Guid(nullable: false),
                        ProvidingFacilitiesDeductionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmissionId, t.ProvidingFacilitiesDeductionTypeId });
            
            AddColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "ReviewedDocumentation", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "ProvidingFacilitiesDeductionTypeOther", c => c.String());
            AddColumn("dbo.ApplicationSubmissions", "ProvidingFacilities", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "NumEmployeesRepresentativePayee", c => c.Int());
            AddColumn("dbo.ApplicationSubmissions", "RepresentativePayeeSocialSecurityBenefits", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.EmployerInfoFacilitiesDeductionType", "EmployerInfoId", "dbo.EmployerInfoes");
            DropForeignKey("dbo.EmployerInfoFacilitiesDeductionType", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses");
            DropIndex("dbo.EmployerInfoFacilitiesDeductionType", new[] { "ProvidingFacilitiesDeductionTypeId" });
            DropIndex("dbo.EmployerInfoFacilitiesDeductionType", new[] { "EmployerInfoId" });
            DropTable("dbo.EmployerInfoFacilitiesDeductionType");
            CreateIndex("dbo.AppSubmissionFacilitiesDeductionType", "ProvidingFacilitiesDeductionTypeId");
            CreateIndex("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmissionId");
            CreateIndex("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeId");
            AddForeignKey("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmissionId", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
        }
    }
}
