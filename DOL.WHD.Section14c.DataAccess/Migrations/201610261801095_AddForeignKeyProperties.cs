namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "Response_Id", "dbo.Responses");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSite_Id", "dbo.WorkSites");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "Response_Id", "dbo.Responses");
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
                        WorkSite_Id = c.Guid(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkSite_Id, t.Response_Id });
            
            CreateTable(
                "dbo.AppSubmissionFacilitiesDeductionType",
                c => new
                    {
                        ApplicationSubmission_Id = c.Guid(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmission_Id, t.Response_Id });
            
            CreateTable(
                "dbo.AppSubmissionEstablishmentType",
                c => new
                    {
                        ApplicationSubmission_Id = c.Guid(nullable: false),
                        Response_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmission_Id, t.Response_Id });
            
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSiteTypeId", "dbo.Responses");
            DropForeignKey("dbo.WorkSiteWorkSiteType", "WorkSiteId", "dbo.WorkSites");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmissionId", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "EstablishmentTypeId", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmissionId", "dbo.ApplicationSubmissions");
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSiteTypeId" });
            DropIndex("dbo.WorkSiteWorkSiteType", new[] { "WorkSiteId" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ProvidingFacilitiesDeductionTypeId" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmissionId" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "EstablishmentTypeId" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmissionId" });
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
            AddForeignKey("dbo.WorkSiteWorkSiteType", "Response_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkSiteWorkSiteType", "WorkSite_Id", "dbo.WorkSites", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "Response_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionEstablishmentType", "Response_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
        }
    }
}
