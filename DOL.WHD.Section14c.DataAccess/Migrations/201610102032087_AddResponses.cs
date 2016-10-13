namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResponses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionKey = c.String(nullable: false),
                        Display = c.String(nullable: false),
                        SubDisplay = c.String(),
                        OtherValueKey = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppSubmissionEstablishmentType",
                c => new
                    {
                        ApplicationSubmission_Id = c.Int(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmission_Id, t.Response_Id })
                .ForeignKey("dbo.ApplicationSubmissions", t => t.ApplicationSubmission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.Response_Id, cascadeDelete: true)
                .Index(t => t.ApplicationSubmission_Id)
                .Index(t => t.Response_Id);
            
            CreateTable(
                "dbo.AppSubmissionFacilitiesDeductionType",
                c => new
                    {
                        ApplicationSubmission_Id = c.Int(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmission_Id, t.Response_Id })
                .ForeignKey("dbo.ApplicationSubmissions", t => t.ApplicationSubmission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.Response_Id, cascadeDelete: true)
                .Index(t => t.ApplicationSubmission_Id)
                .Index(t => t.Response_Id);
            
            CreateTable(
                "dbo.WorkSiteWorkSiteType",
                c => new
                    {
                        WorkSite_Id = c.Int(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkSite_Id, t.Response_Id })
                .ForeignKey("dbo.WorkSites", t => t.WorkSite_Id, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.Response_Id, cascadeDelete: true)
                .Index(t => t.WorkSite_Id)
                .Index(t => t.Response_Id);
            
            AddColumn("dbo.ApplicationSubmissions", "ProvidingFacilitiesDeductionTypeOther", c => c.String());
            AddColumn("dbo.ApplicationSubmissions", "ApplicationType_Id", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "PayType_Id", c => c.Int(nullable: false));
            AddColumn("dbo.EmployerInfoes", "EmployerStatusOther", c => c.String());
            AddColumn("dbo.EmployerInfoes", "EmployerStatus_Id", c => c.Int(nullable: false));
            AddColumn("dbo.EmployerInfoes", "EO13658_Id", c => c.Int(nullable: false));
            AddColumn("dbo.EmployerInfoes", "SCA_Id", c => c.Int(nullable: false));
            AddColumn("dbo.WageTypeInfoes", "PrevailingWageMethod_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "PrimaryDisabilityOther", c => c.String());
            AddColumn("dbo.Employees", "PrimaryDisability_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ApplicationSubmissions", "ApplicationType_Id");
            CreateIndex("dbo.ApplicationSubmissions", "PayType_Id");
            CreateIndex("dbo.EmployerInfoes", "EmployerStatus_Id");
            CreateIndex("dbo.EmployerInfoes", "EO13658_Id");
            CreateIndex("dbo.EmployerInfoes", "SCA_Id");
            CreateIndex("dbo.WageTypeInfoes", "PrevailingWageMethod_Id");
            CreateIndex("dbo.Employees", "PrimaryDisability_Id");
            AddForeignKey("dbo.ApplicationSubmissions", "ApplicationType_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployerInfoes", "EmployerStatus_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployerInfoes", "EO13658_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployerInfoes", "SCA_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationSubmissions", "PayType_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WageTypeInfoes", "PrevailingWageMethod_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "PrimaryDisability_Id", "dbo.Responses", "Id", cascadeDelete: true);
            DropColumn("dbo.ApplicationSubmissions", "ApplicationType");
            DropColumn("dbo.ApplicationSubmissions", "PayType");
            DropColumn("dbo.ApplicationSubmissions", "ProvidingFacilitiesDeductionType");
            DropColumn("dbo.EmployerInfoes", "Status");
            DropColumn("dbo.EmployerInfoes", "SCA");
            DropColumn("dbo.EmployerInfoes", "EO13658");
            DropColumn("dbo.WageTypeInfoes", "PrevailingWageMethod");
            DropColumn("dbo.WorkSites", "Type");
            DropColumn("dbo.Employees", "PrimaryDisability");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "PrimaryDisability", c => c.String(nullable: false));
            AddColumn("dbo.WorkSites", "Type", c => c.String(nullable: false));
            AddColumn("dbo.WageTypeInfoes", "PrevailingWageMethod", c => c.String(nullable: false));
            AddColumn("dbo.EmployerInfoes", "EO13658", c => c.Int(nullable: false));
            AddColumn("dbo.EmployerInfoes", "SCA", c => c.Int(nullable: false));
            AddColumn("dbo.EmployerInfoes", "Status", c => c.String(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "ProvidingFacilitiesDeductionType", c => c.String());
            AddColumn("dbo.ApplicationSubmissions", "PayType", c => c.String(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "ApplicationType", c => c.String(nullable: false));
            DropForeignKey("dbo.WorkSiteWorkSiteType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSite_Id", "dbo.WorkSites");
            DropForeignKey("dbo.Employees", "PrimaryDisability_Id", "dbo.Responses");
            DropForeignKey("dbo.WageTypeInfoes", "PrevailingWageMethod_Id", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.ApplicationSubmissions", "PayType_Id", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.EmployerInfoes", "SCA_Id", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoes", "EO13658_Id", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoes", "EmployerStatus_Id", "dbo.Responses");
            DropForeignKey("dbo.ApplicationSubmissions", "ApplicationType_Id", "dbo.Responses");
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "Response_Id" });
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSite_Id" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "Response_Id" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "Response_Id" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.Employees", new[] { "PrimaryDisability_Id" });
            DropIndex("dbo.WageTypeInfoes", new[] { "PrevailingWageMethod_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "SCA_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "EO13658_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "EmployerStatus_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "PayType_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "ApplicationType_Id" });
            DropColumn("dbo.Employees", "PrimaryDisability_Id");
            DropColumn("dbo.Employees", "PrimaryDisabilityOther");
            DropColumn("dbo.WageTypeInfoes", "PrevailingWageMethod_Id");
            DropColumn("dbo.EmployerInfoes", "SCA_Id");
            DropColumn("dbo.EmployerInfoes", "EO13658_Id");
            DropColumn("dbo.EmployerInfoes", "EmployerStatus_Id");
            DropColumn("dbo.EmployerInfoes", "EmployerStatusOther");
            DropColumn("dbo.ApplicationSubmissions", "PayType_Id");
            DropColumn("dbo.ApplicationSubmissions", "ApplicationType_Id");
            DropColumn("dbo.ApplicationSubmissions", "ProvidingFacilitiesDeductionTypeOther");
            DropTable("dbo.WorkSiteWorkSiteType");
            DropTable("dbo.AppSubmissionFacilitiesDeductionType");
            DropTable("dbo.AppSubmissionEstablishmentType");
            DropTable("dbo.Responses");
        }
    }
}
