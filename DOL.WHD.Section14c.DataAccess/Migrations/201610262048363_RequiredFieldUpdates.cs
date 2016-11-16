namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFieldUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HourlyWageInfoes", "AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.PieceRateWageInfoes", "AttachmentId", "dbo.Attachments");
            DropIndex("dbo.HourlyWageInfoes", new[] { "AttachmentId" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "AttachmentId" });
            AlterColumn("dbo.EmployerInfoes", "TradeName", c => c.String());
            AlterColumn("dbo.EmployerInfoes", "PriorLegalName", c => c.String());
            AlterColumn("dbo.HourlyWageInfoes", "AttachmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PieceRateWageInfoes", "AttachmentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.HourlyWageInfoes", "AttachmentId");
            CreateIndex("dbo.PieceRateWageInfoes", "AttachmentId");
            AddForeignKey("dbo.HourlyWageInfoes", "AttachmentId", "dbo.Attachments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PieceRateWageInfoes", "AttachmentId", "dbo.Attachments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PieceRateWageInfoes", "AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.HourlyWageInfoes", "AttachmentId", "dbo.Attachments");
            DropIndex("dbo.PieceRateWageInfoes", new[] { "AttachmentId" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "AttachmentId" });
            AlterColumn("dbo.PieceRateWageInfoes", "AttachmentId", c => c.Guid());
            AlterColumn("dbo.HourlyWageInfoes", "AttachmentId", c => c.Guid());
            AlterColumn("dbo.EmployerInfoes", "PriorLegalName", c => c.String(nullable: false));
            AlterColumn("dbo.EmployerInfoes", "TradeName", c => c.String(nullable: false));
            CreateIndex("dbo.PieceRateWageInfoes", "AttachmentId");
            CreateIndex("dbo.HourlyWageInfoes", "AttachmentId");
            AddForeignKey("dbo.PieceRateWageInfoes", "AttachmentId", "dbo.Attachments", "Id");
            AddForeignKey("dbo.HourlyWageInfoes", "AttachmentId", "dbo.Attachments", "Id");
        }
    }
}
