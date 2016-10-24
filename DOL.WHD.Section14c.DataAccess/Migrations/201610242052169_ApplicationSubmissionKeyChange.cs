namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationSubmissionKeyChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropIndex("dbo.WorkSites", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmission_Id" });
            DropPrimaryKey("dbo.ApplicationSubmissions");
            DropPrimaryKey("dbo.AppSubmissionEstablishmentType");
            DropPrimaryKey("dbo.AppSubmissionFacilitiesDeductionType");
            AddColumn("dbo.ApplicationSubmissions", "EIN", c => c.String(nullable: false));
            DropColumn("dbo.ApplicationSubmissions", "Id");
            AddColumn("dbo.ApplicationSubmissions", "Id", c => c.Guid(nullable: false));
            DropColumn("dbo.WorkSites", "ApplicationSubmission_Id");
            AddColumn("dbo.WorkSites", "ApplicationSubmission_Id", c => c.Guid());
            DropColumn("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id");
            AddColumn("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", c => c.Guid(nullable: false));
            DropColumn("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id");
            AddColumn("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ApplicationSubmissions", "Id");
            AddPrimaryKey("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmission_Id", "Response_Id" });
            AddPrimaryKey("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmission_Id", "Response_Id" });
            CreateIndex("dbo.WorkSites", "ApplicationSubmission_Id");
            CreateIndex("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id");
            CreateIndex("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id");
            AddForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id");
            DropColumn("dbo.ApplicationSubmissions", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationSubmissions", "UserId", c => c.String(nullable: false));
            DropForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropIndex("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.WorkSites", new[] { "ApplicationSubmission_Id" });
            DropPrimaryKey("dbo.AppSubmissionFacilitiesDeductionType");
            DropPrimaryKey("dbo.AppSubmissionEstablishmentType");
            DropPrimaryKey("dbo.ApplicationSubmissions");
            DropColumn("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id");
            AddColumn("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", c => c.Int(nullable: false));
            DropColumn("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id");
            AddColumn("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", c => c.Int(nullable: false));
            DropColumn("dbo.WorkSites", "ApplicationSubmission_Id");
            AddColumn("dbo.WorkSites", "ApplicationSubmission_Id", c => c.Int());
            DropColumn("dbo.ApplicationSubmissions", "Id");
            AddColumn("dbo.ApplicationSubmissions", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ApplicationSubmissions", "EIN");
            AddPrimaryKey("dbo.AppSubmissionFacilitiesDeductionType", new[] { "ApplicationSubmission_Id", "Response_Id" });
            AddPrimaryKey("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmission_Id", "Response_Id" });
            AddPrimaryKey("dbo.ApplicationSubmissions", "Id");
            CreateIndex("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id");
            CreateIndex("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id");
            CreateIndex("dbo.WorkSites", "ApplicationSubmission_Id");
            AddForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id");
            AddForeignKey("dbo.AppSubmissionFacilitiesDeductionType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions", "Id", cascadeDelete: true);
        }
    }
}
