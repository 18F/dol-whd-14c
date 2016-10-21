namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttachmentUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OriginalFileName = c.String(nullable: false, maxLength: 255),
                        RepositoryFilePath = c.String(nullable: false, maxLength: 255),
                        FileSize = c.Long(nullable: false),
                        MimeType = c.String(nullable: false, maxLength: 255),
                        Deleted = c.Boolean(nullable: false),
                        ApplicationSave_EIN = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationSaves", t => t.ApplicationSave_EIN)
                .Index(t => t.ApplicationSave_EIN);
            
            AddColumn("dbo.EmployerInfoes", "Attachment_Id", c => c.Guid());
            AddColumn("dbo.WageTypeInfoes", "Attachment_Id", c => c.Guid());
            AddColumn("dbo.WageTypeInfoes", "Attachment_Id1", c => c.Guid());
            AddColumn("dbo.PrevailingWageSurveyInfoes", "Attachment_Id", c => c.Guid());
            CreateIndex("dbo.EmployerInfoes", "Attachment_Id");
            CreateIndex("dbo.WageTypeInfoes", "Attachment_Id");
            CreateIndex("dbo.WageTypeInfoes", "Attachment_Id1");
            CreateIndex("dbo.PrevailingWageSurveyInfoes", "Attachment_Id");
            AddForeignKey("dbo.EmployerInfoes", "Attachment_Id", "dbo.Attachments", "Id");
            AddForeignKey("dbo.PrevailingWageSurveyInfoes", "Attachment_Id", "dbo.Attachments", "Id");
            AddForeignKey("dbo.WageTypeInfoes", "Attachment_Id", "dbo.Attachments", "Id");
            AddForeignKey("dbo.WageTypeInfoes", "Attachment_Id1", "dbo.Attachments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WageTypeInfoes", "Attachment_Id1", "dbo.Attachments");
            DropForeignKey("dbo.WageTypeInfoes", "Attachment_Id", "dbo.Attachments");
            DropForeignKey("dbo.PrevailingWageSurveyInfoes", "Attachment_Id", "dbo.Attachments");
            DropForeignKey("dbo.EmployerInfoes", "Attachment_Id", "dbo.Attachments");
            DropForeignKey("dbo.Attachments", "ApplicationSave_EIN", "dbo.ApplicationSaves");
            DropIndex("dbo.PrevailingWageSurveyInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.WageTypeInfoes", new[] { "Attachment_Id1" });
            DropIndex("dbo.WageTypeInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "Attachment_Id" });
            DropIndex("dbo.Attachments", new[] { "ApplicationSave_EIN" });
            DropColumn("dbo.PrevailingWageSurveyInfoes", "Attachment_Id");
            DropColumn("dbo.WageTypeInfoes", "Attachment_Id1");
            DropColumn("dbo.WageTypeInfoes", "Attachment_Id");
            DropColumn("dbo.EmployerInfoes", "Attachment_Id");
            DropTable("dbo.Attachments");
        }
    }
}
