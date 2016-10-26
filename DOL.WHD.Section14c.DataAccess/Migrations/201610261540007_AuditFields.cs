namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlternateWageDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlternateWorkDescription = c.String(),
                        AlternateDataSourceUsed = c.String(),
                        PrevailingWageProvidedBySource = c.Double(nullable: false),
                        DataRetrieved = c.DateTime(nullable: false),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            AddColumn("dbo.ApplicationSaves", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.ApplicationSaves", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApplicationSaves", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ApplicationSaves", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Attachments", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.Attachments", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Attachments", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Attachments", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.ApplicationSubmissions", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApplicationSubmissions", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ApplicationSubmissions", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Responses", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.Responses", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Responses", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Responses", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmployerInfoes", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.EmployerInfoes", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmployerInfoes", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.EmployerInfoes", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkerCountInfoes", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.WorkerCountInfoes", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkerCountInfoes", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.WorkerCountInfoes", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Addresses", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.Addresses", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Addresses", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Addresses", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.HourlyWageInfoes", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.HourlyWageInfoes", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.HourlyWageInfoes", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.HourlyWageInfoes", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.HourlyWageInfoes", "AlternateWageData_Id", c => c.Int());
            AddColumn("dbo.PrevailingWageSurveyInfoes", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.PrevailingWageSurveyInfoes", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PrevailingWageSurveyInfoes", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.PrevailingWageSurveyInfoes", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.SourceEmployers", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.SourceEmployers", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.SourceEmployers", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.SourceEmployers", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PieceRateWageInfoes", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.PieceRateWageInfoes", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PieceRateWageInfoes", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.PieceRateWageInfoes", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PieceRateWageInfoes", "AlternateWageData_Id", c => c.Int());
            AddColumn("dbo.WIOAs", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.WIOAs", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WIOAs", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.WIOAs", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WIOAWorkers", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.WIOAWorkers", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WIOAWorkers", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.WIOAWorkers", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkSites", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.WorkSites", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkSites", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.WorkSites", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.Employees", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Employees", "LastModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrganizationMemberships", "CreatedBy_Id", c => c.String());
            AddColumn("dbo.OrganizationMemberships", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrganizationMemberships", "LastModifiedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.OrganizationMemberships", "LastModifiedAt", c => c.DateTime(nullable: false));
            CreateIndex("dbo.ApplicationSaves", "LastModifiedBy_Id");
            CreateIndex("dbo.Attachments", "LastModifiedBy_Id");
            CreateIndex("dbo.OrganizationMemberships", "LastModifiedBy_Id");
            CreateIndex("dbo.ApplicationSubmissions", "LastModifiedBy_Id");
            CreateIndex("dbo.Responses", "LastModifiedBy_Id");
            CreateIndex("dbo.EmployerInfoes", "LastModifiedBy_Id");
            CreateIndex("dbo.WorkerCountInfoes", "LastModifiedBy_Id");
            CreateIndex("dbo.Addresses", "LastModifiedBy_Id");
            CreateIndex("dbo.HourlyWageInfoes", "LastModifiedBy_Id");
            CreateIndex("dbo.HourlyWageInfoes", "AlternateWageData_Id");
            CreateIndex("dbo.PrevailingWageSurveyInfoes", "LastModifiedBy_Id");
            CreateIndex("dbo.SourceEmployers", "LastModifiedBy_Id");
            CreateIndex("dbo.PieceRateWageInfoes", "LastModifiedBy_Id");
            CreateIndex("dbo.PieceRateWageInfoes", "AlternateWageData_Id");
            CreateIndex("dbo.WIOAs", "LastModifiedBy_Id");
            CreateIndex("dbo.WIOAWorkers", "LastModifiedBy_Id");
            CreateIndex("dbo.WorkSites", "LastModifiedBy_Id");
            CreateIndex("dbo.Employees", "LastModifiedBy_Id");
            AddForeignKey("dbo.OrganizationMemberships", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Attachments", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ApplicationSaves", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Responses", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.EmployerInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.WorkerCountInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Addresses", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.HourlyWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas", "Id");
            AddForeignKey("dbo.HourlyWageInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrevailingWageSurveyInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SourceEmployers", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PieceRateWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas", "Id");
            AddForeignKey("dbo.PieceRateWageInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.WIOAs", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.WIOAWorkers", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.WorkSites", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employees", "LastModifiedBy_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.HourlyWageInfoes", "AlternateWageData_AlternateWorkDescription");
            DropColumn("dbo.HourlyWageInfoes", "AlternateWageData_AlternateDataSourceUsed");
            DropColumn("dbo.HourlyWageInfoes", "AlternateWageData_PrevailingWageProvidedBySource");
            DropColumn("dbo.HourlyWageInfoes", "AlternateWageData_DataRetrieved");
            DropColumn("dbo.PieceRateWageInfoes", "AlternateWageData_AlternateWorkDescription");
            DropColumn("dbo.PieceRateWageInfoes", "AlternateWageData_AlternateDataSourceUsed");
            DropColumn("dbo.PieceRateWageInfoes", "AlternateWageData_PrevailingWageProvidedBySource");
            DropColumn("dbo.PieceRateWageInfoes", "AlternateWageData_DataRetrieved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PieceRateWageInfoes", "AlternateWageData_DataRetrieved", c => c.DateTime(nullable: false));
            AddColumn("dbo.PieceRateWageInfoes", "AlternateWageData_PrevailingWageProvidedBySource", c => c.Double(nullable: false));
            AddColumn("dbo.PieceRateWageInfoes", "AlternateWageData_AlternateDataSourceUsed", c => c.String());
            AddColumn("dbo.PieceRateWageInfoes", "AlternateWageData_AlternateWorkDescription", c => c.String());
            AddColumn("dbo.HourlyWageInfoes", "AlternateWageData_DataRetrieved", c => c.DateTime(nullable: false));
            AddColumn("dbo.HourlyWageInfoes", "AlternateWageData_PrevailingWageProvidedBySource", c => c.Double(nullable: false));
            AddColumn("dbo.HourlyWageInfoes", "AlternateWageData_AlternateDataSourceUsed", c => c.String());
            AddColumn("dbo.HourlyWageInfoes", "AlternateWageData_AlternateWorkDescription", c => c.String());
            DropForeignKey("dbo.Employees", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkSites", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WIOAWorkers", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WIOAs", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PieceRateWageInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PieceRateWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropForeignKey("dbo.SourceEmployers", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrevailingWageSurveyInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HourlyWageInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HourlyWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropForeignKey("dbo.AlternateWageDatas", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkerCountInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployerInfoes", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationSubmissions", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Responses", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationSaves", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attachments", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrganizationMemberships", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.WorkSites", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.WIOAWorkers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.WIOAs", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "AlternateWageData_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PrevailingWageSurveyInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.AlternateWageDatas", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "AlternateWageData_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Addresses", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.WorkerCountInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Responses", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.OrganizationMemberships", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Attachments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ApplicationSaves", new[] { "LastModifiedBy_Id" });
            DropColumn("dbo.OrganizationMemberships", "LastModifiedAt");
            DropColumn("dbo.OrganizationMemberships", "LastModifiedBy_Id");
            DropColumn("dbo.OrganizationMemberships", "CreatedAt");
            DropColumn("dbo.OrganizationMemberships", "CreatedBy_Id");
            DropColumn("dbo.Employees", "LastModifiedAt");
            DropColumn("dbo.Employees", "LastModifiedBy_Id");
            DropColumn("dbo.Employees", "CreatedAt");
            DropColumn("dbo.Employees", "CreatedBy_Id");
            DropColumn("dbo.WorkSites", "LastModifiedAt");
            DropColumn("dbo.WorkSites", "LastModifiedBy_Id");
            DropColumn("dbo.WorkSites", "CreatedAt");
            DropColumn("dbo.WorkSites", "CreatedBy_Id");
            DropColumn("dbo.WIOAWorkers", "LastModifiedAt");
            DropColumn("dbo.WIOAWorkers", "LastModifiedBy_Id");
            DropColumn("dbo.WIOAWorkers", "CreatedAt");
            DropColumn("dbo.WIOAWorkers", "CreatedBy_Id");
            DropColumn("dbo.WIOAs", "LastModifiedAt");
            DropColumn("dbo.WIOAs", "LastModifiedBy_Id");
            DropColumn("dbo.WIOAs", "CreatedAt");
            DropColumn("dbo.WIOAs", "CreatedBy_Id");
            DropColumn("dbo.PieceRateWageInfoes", "AlternateWageData_Id");
            DropColumn("dbo.PieceRateWageInfoes", "LastModifiedAt");
            DropColumn("dbo.PieceRateWageInfoes", "LastModifiedBy_Id");
            DropColumn("dbo.PieceRateWageInfoes", "CreatedAt");
            DropColumn("dbo.PieceRateWageInfoes", "CreatedBy_Id");
            DropColumn("dbo.SourceEmployers", "LastModifiedAt");
            DropColumn("dbo.SourceEmployers", "LastModifiedBy_Id");
            DropColumn("dbo.SourceEmployers", "CreatedAt");
            DropColumn("dbo.SourceEmployers", "CreatedBy_Id");
            DropColumn("dbo.PrevailingWageSurveyInfoes", "LastModifiedAt");
            DropColumn("dbo.PrevailingWageSurveyInfoes", "LastModifiedBy_Id");
            DropColumn("dbo.PrevailingWageSurveyInfoes", "CreatedAt");
            DropColumn("dbo.PrevailingWageSurveyInfoes", "CreatedBy_Id");
            DropColumn("dbo.HourlyWageInfoes", "AlternateWageData_Id");
            DropColumn("dbo.HourlyWageInfoes", "LastModifiedAt");
            DropColumn("dbo.HourlyWageInfoes", "LastModifiedBy_Id");
            DropColumn("dbo.HourlyWageInfoes", "CreatedAt");
            DropColumn("dbo.HourlyWageInfoes", "CreatedBy_Id");
            DropColumn("dbo.Addresses", "LastModifiedAt");
            DropColumn("dbo.Addresses", "LastModifiedBy_Id");
            DropColumn("dbo.Addresses", "CreatedAt");
            DropColumn("dbo.Addresses", "CreatedBy_Id");
            DropColumn("dbo.WorkerCountInfoes", "LastModifiedAt");
            DropColumn("dbo.WorkerCountInfoes", "LastModifiedBy_Id");
            DropColumn("dbo.WorkerCountInfoes", "CreatedAt");
            DropColumn("dbo.WorkerCountInfoes", "CreatedBy_Id");
            DropColumn("dbo.EmployerInfoes", "LastModifiedAt");
            DropColumn("dbo.EmployerInfoes", "LastModifiedBy_Id");
            DropColumn("dbo.EmployerInfoes", "CreatedAt");
            DropColumn("dbo.EmployerInfoes", "CreatedBy_Id");
            DropColumn("dbo.Responses", "LastModifiedAt");
            DropColumn("dbo.Responses", "LastModifiedBy_Id");
            DropColumn("dbo.Responses", "CreatedAt");
            DropColumn("dbo.Responses", "CreatedBy_Id");
            DropColumn("dbo.ApplicationSubmissions", "LastModifiedAt");
            DropColumn("dbo.ApplicationSubmissions", "LastModifiedBy_Id");
            DropColumn("dbo.ApplicationSubmissions", "CreatedAt");
            DropColumn("dbo.ApplicationSubmissions", "CreatedBy_Id");
            DropColumn("dbo.Attachments", "LastModifiedAt");
            DropColumn("dbo.Attachments", "LastModifiedBy_Id");
            DropColumn("dbo.Attachments", "CreatedAt");
            DropColumn("dbo.Attachments", "CreatedBy_Id");
            DropColumn("dbo.ApplicationSaves", "LastModifiedAt");
            DropColumn("dbo.ApplicationSaves", "LastModifiedBy_Id");
            DropColumn("dbo.ApplicationSaves", "CreatedAt");
            DropColumn("dbo.ApplicationSaves", "CreatedBy_Id");
            DropTable("dbo.AlternateWageDatas");
        }
    }
}
