namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WageDataModelUpdateAddWageInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HourlyWageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkMeasurementFrequency = c.String(nullable: false),
                        NumWorkers = c.Int(nullable: false),
                        JobName = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        AlternateWageData_AlternateWorkDescription = c.String(),
                        AlternateWageData_AlternateDataSourceUsed = c.String(),
                        AlternateWageData_PrevailingWageProvidedBySource = c.Double(nullable: false),
                        AlternateWageData_DataRetrieved = c.DateTime(nullable: false),
                        Attachment_Id = c.Guid(),
                        MostRecentPrevailingWageSurvey_Id = c.Int(),
                        PrevailingWageMethod_Id = c.Int(nullable: false),
                        SCAWageDetermination_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attachments", t => t.Attachment_Id)
                .ForeignKey("dbo.PrevailingWageSurveyInfoes", t => t.MostRecentPrevailingWageSurvey_Id)
                .ForeignKey("dbo.Responses", t => t.PrevailingWageMethod_Id, cascadeDelete: true)
                .ForeignKey("dbo.Attachments", t => t.SCAWageDetermination_Id)
                .Index(t => t.Attachment_Id)
                .Index(t => t.MostRecentPrevailingWageSurvey_Id)
                .Index(t => t.PrevailingWageMethod_Id)
                .Index(t => t.SCAWageDetermination_Id);
            
            CreateTable(
                "dbo.PrevailingWageSurveyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrevailingWageDetermined = c.Double(nullable: false),
                        Attachment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attachments", t => t.Attachment_Id)
                .Index(t => t.Attachment_Id);
            
            CreateTable(
                "dbo.SourceEmployers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployerName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        ContactName = c.String(nullable: false),
                        ContactTitle = c.String(nullable: false),
                        ContactDate = c.DateTime(nullable: false),
                        JobDescription = c.String(nullable: false),
                        ExperiencedWorkerWageProvided = c.String(nullable: false),
                        ConclusionWageRateNotBasedOnEntry = c.String(nullable: false),
                        Address_Id = c.Int(nullable: false),
                        PrevailingWageSurveyInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id, cascadeDelete: true)
                .ForeignKey("dbo.PrevailingWageSurveyInfoes", t => t.PrevailingWageSurveyInfo_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.PrevailingWageSurveyInfo_Id);
            
            CreateTable(
                "dbo.PieceRateWageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PieceRateWorkDescription = c.String(nullable: false),
                        PrevailingWageDeterminedForJob = c.Double(nullable: false),
                        StandardProductivity = c.Double(nullable: false),
                        PieceRatePaidToWorkers = c.Double(nullable: false),
                        NumWorkers = c.Int(nullable: false),
                        JobName = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        AlternateWageData_AlternateWorkDescription = c.String(),
                        AlternateWageData_AlternateDataSourceUsed = c.String(),
                        AlternateWageData_PrevailingWageProvidedBySource = c.Double(nullable: false),
                        AlternateWageData_DataRetrieved = c.DateTime(nullable: false),
                        Attachment_Id = c.Guid(),
                        MostRecentPrevailingWageSurvey_Id = c.Int(),
                        PrevailingWageMethod_Id = c.Int(nullable: false),
                        SCAWageDetermination_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attachments", t => t.Attachment_Id)
                .ForeignKey("dbo.PrevailingWageSurveyInfoes", t => t.MostRecentPrevailingWageSurvey_Id)
                .ForeignKey("dbo.Responses", t => t.PrevailingWageMethod_Id, cascadeDelete: true)
                .ForeignKey("dbo.Attachments", t => t.SCAWageDetermination_Id)
                .Index(t => t.Attachment_Id)
                .Index(t => t.MostRecentPrevailingWageSurvey_Id)
                .Index(t => t.PrevailingWageMethod_Id)
                .Index(t => t.SCAWageDetermination_Id);
            
            AddColumn("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", c => c.Int());
            AddColumn("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", c => c.Int());
            CreateIndex("dbo.ApplicationSubmissions", "HourlyWageInfo_Id");
            CreateIndex("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id");
            AddForeignKey("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", "dbo.HourlyWageInfoes", "Id");
            AddForeignKey("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", "dbo.PieceRateWageInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", "dbo.PieceRateWageInfoes");
            DropForeignKey("dbo.PieceRateWageInfoes", "SCAWageDetermination_Id", "dbo.Attachments");
            DropForeignKey("dbo.PieceRateWageInfoes", "PrevailingWageMethod_Id", "dbo.Responses");
            DropForeignKey("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.PieceRateWageInfoes", "Attachment_Id", "dbo.Attachments");
            DropForeignKey("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", "dbo.HourlyWageInfoes");
            DropForeignKey("dbo.HourlyWageInfoes", "SCAWageDetermination_Id", "dbo.Attachments");
            DropForeignKey("dbo.HourlyWageInfoes", "PrevailingWageMethod_Id", "dbo.Responses");
            DropForeignKey("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.PrevailingWageSurveyInfoes", "Attachment_Id", "dbo.Attachments");
            DropForeignKey("dbo.HourlyWageInfoes", "Attachment_Id", "dbo.Attachments");
            DropIndex("dbo.PieceRateWageInfoes", new[] { "SCAWageDetermination_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "PrevailingWageMethod_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "PrevailingWageSurveyInfo_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "Address_Id" });
            DropIndex("dbo.PrevailingWageSurveyInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "SCAWageDetermination_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "PrevailingWageMethod_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "PieceRateWageInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "HourlyWageInfo_Id" });
            DropColumn("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id");
            DropColumn("dbo.ApplicationSubmissions", "HourlyWageInfo_Id");
            DropTable("dbo.PieceRateWageInfoes");
            DropTable("dbo.SourceEmployers");
            DropTable("dbo.PrevailingWageSurveyInfoes");
            DropTable("dbo.HourlyWageInfoes");
        }
    }
}
