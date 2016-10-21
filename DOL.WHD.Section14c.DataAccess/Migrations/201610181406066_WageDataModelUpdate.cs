namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WageDataModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrevailingWageSurveyInfoes", "Attachment_Id", "dbo.Attachments");
            DropForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.WageTypeInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.WageTypeInfoes", "PrevailingWageMethod_Id", "dbo.Responses");
            DropForeignKey("dbo.WageTypeInfoes", "Attachment_Id", "dbo.Attachments");
            DropForeignKey("dbo.WageTypeInfoes", "Attachment_Id1", "dbo.Attachments");
            DropForeignKey("dbo.ApplicationSubmissions", "WageTypeInfo_Id", "dbo.WageTypeInfoes");
            DropIndex("dbo.ApplicationSubmissions", new[] { "WageTypeInfo_Id" });
            DropIndex("dbo.WageTypeInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.WageTypeInfoes", new[] { "PrevailingWageMethod_Id" });
            DropIndex("dbo.WageTypeInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.WageTypeInfoes", new[] { "Attachment_Id1" });
            DropIndex("dbo.PrevailingWageSurveyInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "Address_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "PrevailingWageSurveyInfo_Id" });
            DropColumn("dbo.ApplicationSubmissions", "TemporaryAuthority");
            DropColumn("dbo.ApplicationSubmissions", "WageTypeInfo_Id");
            DropTable("dbo.WageTypeInfoes");
            DropTable("dbo.PrevailingWageSurveyInfoes");
            DropTable("dbo.SourceEmployers");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrevailingWageSurveyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BasedOnSurvey = c.String(nullable: false),
                        Attachment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WageTypeInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumWorkers = c.Int(nullable: false),
                        JobName = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        FrequencyOfWorkMeasurements = c.String(),
                        NumPieceRateWorkers = c.Int(),
                        WorkDescription = c.String(),
                        WageRatePerHour = c.Double(),
                        ProductivityUnitsPerHour = c.Double(),
                        PieceRatePerUnit = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        MostRecentPrevailingWageSurvey_Id = c.Int(),
                        PrevailingWageMethod_Id = c.Int(nullable: false),
                        Attachment_Id = c.Guid(),
                        Attachment_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ApplicationSubmissions", "WageTypeInfo_Id", c => c.Int());
            AddColumn("dbo.ApplicationSubmissions", "TemporaryAuthority", c => c.Boolean(nullable: false));
            CreateIndex("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id");
            CreateIndex("dbo.SourceEmployers", "Address_Id");
            CreateIndex("dbo.PrevailingWageSurveyInfoes", "Attachment_Id");
            CreateIndex("dbo.WageTypeInfoes", "Attachment_Id1");
            CreateIndex("dbo.WageTypeInfoes", "Attachment_Id");
            CreateIndex("dbo.WageTypeInfoes", "PrevailingWageMethod_Id");
            CreateIndex("dbo.WageTypeInfoes", "MostRecentPrevailingWageSurvey_Id");
            CreateIndex("dbo.ApplicationSubmissions", "WageTypeInfo_Id");
            AddForeignKey("dbo.ApplicationSubmissions", "WageTypeInfo_Id", "dbo.WageTypeInfoes", "Id");
            AddForeignKey("dbo.WageTypeInfoes", "Attachment_Id1", "dbo.Attachments", "Id");
            AddForeignKey("dbo.WageTypeInfoes", "Attachment_Id", "dbo.Attachments", "Id");
            AddForeignKey("dbo.WageTypeInfoes", "PrevailingWageMethod_Id", "dbo.Responses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WageTypeInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes", "Id");
            AddForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrevailingWageSurveyInfoes", "Attachment_Id", "dbo.Attachments", "Id");
        }
    }
}
