namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttachmentDecoupleSave : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachments", "ApplicationSave_EIN", "dbo.ApplicationSaves");
            DropIndex("dbo.Attachments", new[] { "ApplicationSave_EIN" });
            RenameColumn("dbo.Attachments", "ApplicationSave_EIN", "EIN");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Attachments", "EIN", "ApplicationSave_EIN");
            CreateIndex("dbo.Attachments", "ApplicationSave_EIN");
            AddForeignKey("dbo.Attachments", "ApplicationSave_EIN", "dbo.ApplicationSaves", "EIN");
        }
    }
}
