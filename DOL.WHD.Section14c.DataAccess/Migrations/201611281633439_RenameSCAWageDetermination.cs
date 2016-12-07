namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSCAWageDetermination : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "SCAWageDeterminationId", newName: "SCAWageDeterminationAttachmentId");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "SCAWageDeterminationId", newName: "SCAWageDeterminationAttachmentId");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_SCAWageDeterminationId", newName: "IX_SCAWageDeterminationAttachmentId");
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_SCAWageDeterminationId", newName: "IX_SCAWageDeterminationAttachmentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PieceRateWageInfoes", name: "IX_SCAWageDeterminationAttachmentId", newName: "IX_SCAWageDeterminationId");
            RenameIndex(table: "dbo.HourlyWageInfoes", name: "IX_SCAWageDeterminationAttachmentId", newName: "IX_SCAWageDeterminationId");
            RenameColumn(table: "dbo.PieceRateWageInfoes", name: "SCAWageDeterminationAttachmentId", newName: "SCAWageDeterminationId");
            RenameColumn(table: "dbo.HourlyWageInfoes", name: "SCAWageDeterminationAttachmentId", newName: "SCAWageDeterminationId");
        }
    }
}
