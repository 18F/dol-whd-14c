namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSite_Id", "dbo.WorkSites");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.ApplicationSubmissions", "Employer_Id", "dbo.EmployerInfoes");
            DropForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes");
            DropForeignKey("dbo.EmployerInfoes", "ParentAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.EmployerInfoes", "PhysicalAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.WorkSites", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", "dbo.HourlyWageInfoes");
            DropForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", "dbo.PieceRateWageInfoes");
            DropForeignKey("dbo.WIOAWorkers", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.ApplicationSubmissions", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.Employees", "WorkSite_Id", "dbo.WorkSites");
            DropIndex("dbo.ApplicationSubmissions", new[] { "Employer_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "HourlyWageInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "PieceRateWageInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "WIOA_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "NumSubminimalWageWorkers_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "ParentAddress_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "PhysicalAddress_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "Address_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "PrevailingWageSurveyInfo_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.WIOAWorkers", new[] { "WIOA_Id" });
            DropIndex("dbo.WorkSites", new[] { "Address_Id" });
            DropIndex("dbo.WorkSites", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.Employees", new[] { "WorkSite_Id" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "Response_Id" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "Response_Id" });
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSite_Id" });
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "Response_Id" });
            RenameColumn(table: "dbo.ApplicationSubmissions", name: "ApplicationType_Id", newName: "ApplicationTypeId");
            RenameColumn(table: "dbo.ApplicationSubmissions", name: "PayType_Id", newName: "PayTypeId");
            RenameColumn(table: "dbo.EmployerInfoes", name: "EmployerStatus_Id", newName: "EmployerStatusId");
            RenameColumn(table: "dbo.EmployerInfoes", name: "EO13658_Id", newName: "EO13658Id");
            RenameColumn(table: "dbo.EmployerInfoes", name: "ProvidingFacilitiesDeductionType_Id", newName: "ProvidingFacilitiesDeductionTypeId");
            RenameColumn(table: "dbo.EmployerInfoes", name: "SCA_Id", newName: "SCAId");
            RenameColumn(table: "dbo.EmployerInfoes", name: "SCAAttachment_Id", newName: "SCAAttachmentId");
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "Attachment_Id", newName: "AttachmentId");
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "PrevailingWageMethod_Id", newName: "PrevailingWageMethodId");
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "SCAWageDetermination_Id", newName: "SCAWageDeterminationId");
            RenameColumn(table: "dbo.PrevailingWageSurveyInfoes", name: "Attachment_Id", newName: "AttachmentId");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "Attachment_Id", newName: "AttachmentId");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "PrevailingWageMethod_Id", newName: "PrevailingWageMethodId");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "SCAWageDetermination_Id", newName: "SCAWageDeterminationId");
            RenameColumn(table: "dbo.WIOAWorkers", name: "WIOAWorkerVerified_Id", newName: "WIOAWorkerVerifiedId");
            RenameColumn(table: "dbo.Employees", name: "PrimaryDisability_Id", newName: "PrimaryDisabilityId");
            RenameIndex(table: "dbo.ApplicationSubmissions", name: "IX_ApplicationType_Id", newName: "IX_ApplicationTypeId");
            RenameIndex(table: "dbo.ApplicationSubmissions", name: "IX_PayType_Id", newName: "IX_PayTypeId");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_EmployerStatus_Id", newName: "IX_EmployerStatusId");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_SCA_Id", newName: "IX_SCAId");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_SCAAttachment_Id", newName: "IX_SCAAttachmentId");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_EO13658_Id", newName: "IX_EO13658Id");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_ProvidingFacilitiesDeductionType_Id", newName: "IX_ProvidingFacilitiesDeductionTypeId");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_PrevailingWageMethod_Id", newName: "IX_PrevailingWageMethodId");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_SCAWageDetermination_Id", newName: "IX_SCAWageDeterminationId");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_Attachment_Id", newName: "IX_AttachmentId");
            RenameIndex(table: "dbo.PrevailingWageSurveyInfoes", name: "IX_Attachment_Id", newName: "IX_AttachmentId");
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_PrevailingWageMethod_Id", newName: "IX_PrevailingWageMethodId");
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_SCAWageDetermination_Id", newName: "IX_SCAWageDeterminationId");
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_Attachment_Id", newName: "IX_AttachmentId");
            RenameIndex(table: "dbo.WIOAWorkers", name: "IX_WIOAWorkerVerified_Id", newName: "IX_WIOAWorkerVerifiedId");
            RenameIndex(table: "dbo.Employees", name: "IX_PrimaryDisability_Id", newName: "IX_PrimaryDisabilityId");
            DropPrimaryKey("dbo.ApplicationSubmissions");
            DropPrimaryKey("dbo.EmployerInfoes");
            DropPrimaryKey("dbo.WorkerCountInfoes");
            DropPrimaryKey("dbo.Addresses");
            DropPrimaryKey("dbo.HourlyWageInfoes");
            DropPrimaryKey("dbo.PrevailingWageSurveyInfoes");
            DropPrimaryKey("dbo.SourceEmployers");
            DropPrimaryKey("dbo.PieceRateWageInfoes");
            DropPrimaryKey("dbo.WIOAs");
            DropPrimaryKey("dbo.WIOAWorkers");
            DropPrimaryKey("dbo.WorkSites");
            DropPrimaryKey("dbo.Employees");
            DropTable("dbo.AppSubmissionEstablishmentType");
            DropTable("dbo.AppSubmissionFacilitiesDeductionType");
            DropTable("dbo.WorkSiteWorkSiteType");
            CreateTable(
                "dbo.AppSubmissionEstablishmentType",
                c => new
                    {
                        ApplicationSubmissionId = c.Guid(nullable: false),
                        EstablishmentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmissionId, t.EstablishmentTypeId })
                .ForeignKey("dbo.ApplicationSubmissions", t => t.ApplicationSubmissionId, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.EstablishmentTypeId, cascadeDelete: true)
                .Index(t => t.ApplicationSubmissionId)
                .Index(t => t.EstablishmentTypeId);
            
            CreateTable(
                "dbo.AppSubmissionFacilitiesDeductionType",
                c => new
                    {
                        ApplicationSubmissionId = c.Guid(nullable: false),
                        ProvidingFacilitiesDeductionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmissionId, t.ProvidingFacilitiesDeductionTypeId })
                .ForeignKey("dbo.ApplicationSubmissions", t => t.ApplicationSubmissionId, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.ProvidingFacilitiesDeductionTypeId, cascadeDelete: true)
                .Index(t => t.ApplicationSubmissionId)
                .Index(t => t.ProvidingFacilitiesDeductionTypeId);
            
            CreateTable(
                "dbo.WorkSiteWorkSiteType",
                c => new
                    {
                        WorkSiteId = c.Guid(nullable: false),
                        WorkSiteTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkSiteId, t.WorkSiteTypeId })
                .ForeignKey("dbo.WorkSites", t => t.WorkSiteId, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.WorkSiteTypeId, cascadeDelete: true)
                .Index(t => t.WorkSiteId)
                .Index(t => t.WorkSiteTypeId);
            
            AddColumn("dbo.ApplicationSubmissions", "EIN", c => c.String(nullable: false));
            DropColumn("dbo.ApplicationSubmissions", "Id");
            DropColumn("dbo.ApplicationSubmissions", "Employer_Id");
            DropColumn("dbo.ApplicationSubmissions", "HourlyWageInfo_Id");
            DropColumn("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id");
            DropColumn("dbo.ApplicationSubmissions", "WIOA_Id");
            DropColumn("dbo.EmployerInfoes", "Id");
            DropColumn("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id");
            DropColumn("dbo.EmployerInfoes", "ParentAddress_Id");
            DropColumn("dbo.EmployerInfoes", "PhysicalAddress_Id");
            DropColumn("dbo.WorkerCountInfoes", "Id");
            DropColumn("dbo.Addresses", "Id");
            DropColumn("dbo.HourlyWageInfoes", "Id");
            DropColumn("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            DropColumn("dbo.PrevailingWageSurveyInfoes", "Id");
            DropColumn("dbo.SourceEmployers", "Id");
            DropColumn("dbo.SourceEmployers", "Address_Id");
            DropColumn("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id");
            DropColumn("dbo.PieceRateWageInfoes", "Id");
            DropColumn("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            DropColumn("dbo.WIOAs", "Id");
            DropColumn("dbo.WIOAWorkers", "Id");
            DropColumn("dbo.WIOAWorkers", "WIOA_Id");
            DropColumn("dbo.WorkSites", "Id");
            DropColumn("dbo.WorkSites", "Address_Id");
            DropColumn("dbo.WorkSites", "ApplicationSubmission_Id");
            DropColumn("dbo.Employees", "Id");
            DropColumn("dbo.Employees", "WorkSite_Id");
            AddColumn("dbo.ApplicationSubmissions", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "Employer_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", c => c.Guid());
            AddColumn("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", c => c.Guid());
            AddColumn("dbo.ApplicationSubmissions", "WIOA_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.EmployerInfoes", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.EmployerInfoes", "ParentAddress_Id", c => c.Guid());
            AddColumn("dbo.EmployerInfoes", "PhysicalAddress_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.WorkerCountInfoes", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Addresses", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.HourlyWageInfoes", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", c => c.Guid());
            AddColumn("dbo.PrevailingWageSurveyInfoes", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.SourceEmployers", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.SourceEmployers", "Address_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", c => c.Guid());
            AddColumn("dbo.PieceRateWageInfoes", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", c => c.Guid());
            AddColumn("dbo.WIOAs", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.WIOAWorkers", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.WIOAWorkers", "WIOA_Id", c => c.Guid());
            AddColumn("dbo.WorkSites", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.WorkSites", "Address_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.WorkSites", "ApplicationSubmission_Id", c => c.Guid());
            AddColumn("dbo.Employees", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Employees", "WorkSite_Id", c => c.Guid());
            AddPrimaryKey("dbo.ApplicationSubmissions", "Id");
            AddPrimaryKey("dbo.EmployerInfoes", "Id");
            AddPrimaryKey("dbo.WorkerCountInfoes", "Id");
            AddPrimaryKey("dbo.Addresses", "Id");
            AddPrimaryKey("dbo.HourlyWageInfoes", "Id");
            AddPrimaryKey("dbo.PrevailingWageSurveyInfoes", "Id");
            AddPrimaryKey("dbo.SourceEmployers", "Id");
            AddPrimaryKey("dbo.PieceRateWageInfoes", "Id");
            AddPrimaryKey("dbo.WIOAs", "Id");
            AddPrimaryKey("dbo.WIOAWorkers", "Id");
            AddPrimaryKey("dbo.WorkSites", "Id");
            AddPrimaryKey("dbo.Employees", "Id");
            CreateIndex("dbo.ApplicationSubmissions", "Employer_Id");
            CreateIndex("dbo.ApplicationSubmissions", "HourlyWageInfo_Id");
            CreateIndex("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id");
            CreateIndex("dbo.ApplicationSubmissions", "WIOA_Id");
            CreateIndex("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id");
            CreateIndex("dbo.EmployerInfoes", "ParentAddress_Id");
            CreateIndex("dbo.EmployerInfoes", "PhysicalAddress_Id");
            CreateIndex("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            CreateIndex("dbo.SourceEmployers", "Address_Id");
            CreateIndex("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id");
            CreateIndex("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            CreateIndex("dbo.WIOAWorkers", "WIOA_Id");
            CreateIndex("dbo.WorkSites", "Address_Id");
            CreateIndex("dbo.WorkSites", "ApplicationSubmission_Id");
            CreateIndex("dbo.Employees", "WorkSite_Id");
            AddForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "Employer_Id", "dbo.EmployerInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployerInfoes", "ParentAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.EmployerInfoes", "PhysicalAddress_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkSites", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", "dbo.HourlyWageInfoes", "Id");
            AddForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", "dbo.PieceRateWageInfoes", "Id");
            AddForeignKey("dbo.WIOAWorkers", "WIOA_Id", "dbo.WIOAs", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "WIOA_Id", "dbo.WIOAs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "WorkSite_Id", "dbo.WorkSites", "Id");
            DropColumn("dbo.ApplicationSubmissions", "UserId");
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkSiteWorkSiteType");
            DropTable("dbo.AppSubmissionFacilitiesDeductionType");
            DropTable("dbo.AppSubmissionEstablishmentType");
            CreateTable(
                "dbo.WorkSiteWorkSiteType",
                c => new
                    {
                        WorkSite_Id = c.Int(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkSite_Id, t.Response_Id });
            
            CreateTable(
                "dbo.AppSubmissionFacilitiesDeductionType",
                c => new
                    {
                        ApplicationSubmission_Id = c.Int(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmission_Id, t.Response_Id });
            
            CreateTable(
                "dbo.AppSubmissionEstablishmentType",
                c => new
                    {
                        ApplicationSubmission_Id = c.Int(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmission_Id, t.Response_Id });
            
            AddColumn("dbo.ApplicationSubmissions", "UserId", c => c.String(nullable: false));
            DropForeignKey("dbo.Employees", "WorkSite_Id", "dbo.WorkSites");
            DropForeignKey("dbo.ApplicationSubmissions", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.WIOAWorkers", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", "dbo.PieceRateWageInfoes");
            DropForeignKey("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", "dbo.HourlyWageInfoes");
            DropForeignKey("dbo.WorkSites", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.EmployerInfoes", "PhysicalAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.EmployerInfoes", "ParentAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes");
            DropForeignKey("dbo.ApplicationSubmissions", "Employer_Id", "dbo.EmployerInfoes");
            DropForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSiteTypeId", "dbo.Responses");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSiteId", "dbo.WorkSites");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmissionId", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "EstablishmentTypeId", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmissionId", "dbo.ApplicationSubmissions");
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSiteTypeId" });
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSiteId" });
            DropIndex("dbo.Employees", new[] { "WorkSite_Id" });
            DropIndex("dbo.WorkSites", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.WorkSites", new[] { "Address_Id" });
            DropIndex("dbo.WIOAWorkers", new[] { "WIOA_Id" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ProvidingFacilitiesDeductionTypeId" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmissionId" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "PrevailingWageSurveyInfo_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "Address_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "EstablishmentTypeId" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmissionId" });
            DropIndex("dbo.EmployerInfoes", new[] { "PhysicalAddress_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "ParentAddress_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "NumSubminimalWageWorkers_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "WIOA_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "PieceRateWageInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "HourlyWageInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "Employer_Id" });
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.WorkSites");
            DropPrimaryKey("dbo.WIOAWorkers");
            DropPrimaryKey("dbo.WIOAs");
            DropPrimaryKey("dbo.PieceRateWageInfoes");
            DropPrimaryKey("dbo.SourceEmployers");
            DropPrimaryKey("dbo.PrevailingWageSurveyInfoes");
            DropPrimaryKey("dbo.HourlyWageInfoes");
            DropPrimaryKey("dbo.Addresses");
            DropPrimaryKey("dbo.WorkerCountInfoes");
            DropPrimaryKey("dbo.EmployerInfoes");
            DropPrimaryKey("dbo.ApplicationSubmissions");
            DropColumn("dbo.Employees", "WorkSite_Id");
            DropColumn("dbo.Employees", "Id");
            DropColumn("dbo.WorkSites", "ApplicationSubmission_Id");
            DropColumn("dbo.WorkSites", "Address_Id");
            DropColumn("dbo.WorkSites", "Id");
            DropColumn("dbo.WIOAWorkers", "WIOA_Id");
            DropColumn("dbo.WIOAWorkers", "Id");
            DropColumn("dbo.WIOAs", "Id");
            DropColumn("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            DropColumn("dbo.PieceRateWageInfoes", "Id");
            DropColumn("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id");
            DropColumn("dbo.SourceEmployers", "Address_Id");
            DropColumn("dbo.SourceEmployers", "Id");
            DropColumn("dbo.PrevailingWageSurveyInfoes", "Id");
            DropColumn("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            DropColumn("dbo.HourlyWageInfoes", "Id");
            DropColumn("dbo.Addresses", "Id");
            DropColumn("dbo.WorkerCountInfoes", "Id");
            DropColumn("dbo.EmployerInfoes", "PhysicalAddress_Id");
            DropColumn("dbo.EmployerInfoes", "ParentAddress_Id");
            DropColumn("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id");
            DropColumn("dbo.EmployerInfoes", "Id");
            DropColumn("dbo.ApplicationSubmissions", "WIOA_Id");
            DropColumn("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id");
            DropColumn("dbo.ApplicationSubmissions", "HourlyWageInfo_Id");
            DropColumn("dbo.ApplicationSubmissions", "Employer_Id");
            DropColumn("dbo.ApplicationSubmissions", "Id");
            AddColumn("dbo.Employees", "WorkSite_Id", c => c.Int());
            AddColumn("dbo.Employees", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.WorkSites", "ApplicationSubmission_Id", c => c.Int());
            AddColumn("dbo.WorkSites", "Address_Id", c => c.Int(nullable: false));
            AddColumn("dbo.WorkSites", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.WIOAWorkers", "WIOA_Id", c => c.Int());
            AddColumn("dbo.WIOAWorkers", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.WIOAs", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", c => c.Int());
            AddColumn("dbo.PieceRateWageInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", c => c.Int());
            AddColumn("dbo.SourceEmployers", "Address_Id", c => c.Int(nullable: false));
            AddColumn("dbo.SourceEmployers", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.PrevailingWageSurveyInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", c => c.Int());
            AddColumn("dbo.HourlyWageInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Addresses", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.WorkerCountInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.EmployerInfoes", "PhysicalAddress_Id", c => c.Int(nullable: false));
            AddColumn("dbo.EmployerInfoes", "ParentAddress_Id", c => c.Int());
            AddColumn("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", c => c.Int(nullable: false));
            AddColumn("dbo.EmployerInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ApplicationSubmissions", "WIOA_Id", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", c => c.Int());
            AddColumn("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", c => c.Int());
            AddColumn("dbo.ApplicationSubmissions", "Employer_Id", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ApplicationSubmissions", "EIN");
            AddPrimaryKey("dbo.Employees", "Id");
            AddPrimaryKey("dbo.WorkSites", "Id");
            AddPrimaryKey("dbo.WIOAWorkers", "Id");
            AddPrimaryKey("dbo.WIOAs", "Id");
            AddPrimaryKey("dbo.PieceRateWageInfoes", "Id");
            AddPrimaryKey("dbo.SourceEmployers", "Id");
            AddPrimaryKey("dbo.PrevailingWageSurveyInfoes", "Id");
            AddPrimaryKey("dbo.HourlyWageInfoes", "Id");
            AddPrimaryKey("dbo.Addresses", "Id");
            AddPrimaryKey("dbo.WorkerCountInfoes", "Id");
            AddPrimaryKey("dbo.EmployerInfoes", "Id");
            AddPrimaryKey("dbo.ApplicationSubmissions", "Id");
            RenameIndex(table: "dbo.Employees", name: "IX_PrimaryDisabilityId", newName: "IX_PrimaryDisability_Id");
            RenameIndex(table: "dbo.WIOAWorkers", name: "IX_WIOAWorkerVerifiedId", newName: "IX_WIOAWorkerVerified_Id");
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_AttachmentId", newName: "IX_Attachment_Id");
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_SCAWageDeterminationId", newName: "IX_SCAWageDetermination_Id");
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_PrevailingWageMethodId", newName: "IX_PrevailingWageMethod_Id");
            RenameIndex(table: "dbo.PrevailingWageSurveyInfoes", name: "IX_AttachmentId", newName: "IX_Attachment_Id");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_AttachmentId", newName: "IX_Attachment_Id");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_SCAWageDeterminationId", newName: "IX_SCAWageDetermination_Id");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_PrevailingWageMethodId", newName: "IX_PrevailingWageMethod_Id");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_ProvidingFacilitiesDeductionTypeId", newName: "IX_ProvidingFacilitiesDeductionType_Id");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_EO13658Id", newName: "IX_EO13658_Id");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_SCAAttachmentId", newName: "IX_SCAAttachment_Id");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_SCAId", newName: "IX_SCA_Id");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_EmployerStatusId", newName: "IX_EmployerStatus_Id");
            RenameIndex(table: "dbo.ApplicationSubmissions", name: "IX_PayTypeId", newName: "IX_PayType_Id");
            RenameIndex(table: "dbo.ApplicationSubmissions", name: "IX_ApplicationTypeId", newName: "IX_ApplicationType_Id");
            RenameColumn(table: "dbo.Employees", name: "PrimaryDisabilityId", newName: "PrimaryDisability_Id");
            RenameColumn(table: "dbo.WIOAWorkers", name: "WIOAWorkerVerifiedId", newName: "WIOAWorkerVerified_Id");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "SCAWageDeterminationId", newName: "SCAWageDetermination_Id");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "PrevailingWageMethodId", newName: "PrevailingWageMethod_Id");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "AttachmentId", newName: "Attachment_Id");
            RenameColumn(table: "dbo.PrevailingWageSurveyInfoes", name: "AttachmentId", newName: "Attachment_Id");
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "SCAWageDeterminationId", newName: "SCAWageDetermination_Id");
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "PrevailingWageMethodId", newName: "PrevailingWageMethod_Id");
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "AttachmentId", newName: "Attachment_Id");
            RenameColumn(table: "dbo.EmployerInfoes", name: "SCAAttachmentId", newName: "SCAAttachment_Id");
            RenameColumn(table: "dbo.EmployerInfoes", name: "SCAId", newName: "SCA_Id");
            RenameColumn(table: "dbo.EmployerInfoes", name: "ProvidingFacilitiesDeductionTypeId", newName: "ProvidingFacilitiesDeductionType_Id");
            RenameColumn(table: "dbo.EmployerInfoes", name: "EO13658Id", newName: "EO13658_Id");
            RenameColumn(table: "dbo.EmployerInfoes", name: "EmployerStatusId", newName: "EmployerStatus_Id");
            RenameColumn(table: "dbo.ApplicationSubmissions", name: "PayTypeId", newName: "PayType_Id");
            RenameColumn(table: "dbo.ApplicationSubmissions", name: "ApplicationTypeId", newName: "ApplicationType_Id");
            CreateIndex("dbo.WorkSiteWorkSiteType", "Response_Id");
            CreateIndex("dbo.WorkSiteWorkSiteType", "WorkSite_Id");
            CreateIndex("dbo.AppSubmissionFacilitiesDeductionType", "Response_Id");
            CreateIndex("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id");
            CreateIndex("dbo.AppSubmissionEstablishmentType", "Response_Id");
            CreateIndex("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id");
            CreateIndex("dbo.Employees", "WorkSite_Id");
            CreateIndex("dbo.WorkSites", "ApplicationSubmission_Id");
            CreateIndex("dbo.WorkSites", "Address_Id");
            CreateIndex("dbo.WIOAWorkers", "WIOA_Id");
            CreateIndex("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            CreateIndex("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id");
            CreateIndex("dbo.SourceEmployers", "Address_Id");
            CreateIndex("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id");
            CreateIndex("dbo.EmployerInfoes", "PhysicalAddress_Id");
            CreateIndex("dbo.EmployerInfoes", "ParentAddress_Id");
            CreateIndex("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id");
            CreateIndex("dbo.ApplicationSubmissions", "WIOA_Id");
            CreateIndex("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id");
            CreateIndex("dbo.ApplicationSubmissions", "HourlyWageInfo_Id");
            CreateIndex("dbo.ApplicationSubmissions", "Employer_Id");
            AddForeignKey("dbo.Employees", "WorkSite_Id", "dbo.WorkSites", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "WIOA_Id", "dbo.WIOAs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WIOAWorkers", "WIOA_Id", "dbo.WIOAs", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", "dbo.PieceRateWageInfoes", "Id");
            AddForeignKey("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", "dbo.HourlyWageInfoes", "Id");
            AddForeignKey("dbo.WorkSites", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployerInfoes", "PhysicalAddress_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployerInfoes", "ParentAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationSubmissions", "Employer_Id", "dbo.EmployerInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id");
            AddForeignKey("dbo.WorkSiteWorkSiteType", "Response_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkSiteWorkSiteType", "WorkSite_Id", "dbo.WorkSites", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "Response_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionEstablishmentType", "Response_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
        }
    }
}
