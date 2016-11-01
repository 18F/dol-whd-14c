namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableKeyUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HourlyWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropForeignKey("dbo.PieceRateWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropIndex("dbo.HourlyWageInfoes", new[] { "AlternateWageData_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "AlternateWageData_Id" });
            DropPrimaryKey("dbo.AlternateWageDatas");
            DropColumn("dbo.HourlyWageInfoes", "AlternateWageData_Id");
            DropColumn("dbo.AlternateWageDatas", "Id");
            DropColumn("dbo.PieceRateWageInfoes", "AlternateWageData_Id");
            AddColumn("dbo.HourlyWageInfoes", "AlternateWageData_Id", c => c.Guid());
            AddColumn("dbo.AlternateWageDatas", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PieceRateWageInfoes", "AlternateWageData_Id", c => c.Guid());
            AddPrimaryKey("dbo.AlternateWageDatas", "Id");
            CreateIndex("dbo.HourlyWageInfoes", "AlternateWageData_Id");
            CreateIndex("dbo.PieceRateWageInfoes", "AlternateWageData_Id");
            AddForeignKey("dbo.HourlyWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas", "Id");
            AddForeignKey("dbo.PieceRateWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PieceRateWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropForeignKey("dbo.HourlyWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropIndex("dbo.PieceRateWageInfoes", new[] { "AlternateWageData_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "AlternateWageData_Id" });
            DropPrimaryKey("dbo.AlternateWageDatas");
            DropColumn("dbo.PieceRateWageInfoes", "AlternateWageData_Id");
            DropColumn("dbo.AlternateWageDatas", "Id");
            DropColumn("dbo.HourlyWageInfoes", "AlternateWageData_Id");
            AddColumn("dbo.PieceRateWageInfoes", "AlternateWageData_Id", c => c.Int());
            AddColumn("dbo.AlternateWageDatas", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.HourlyWageInfoes", "AlternateWageData_Id", c => c.Int());
            AddPrimaryKey("dbo.AlternateWageDatas", "Id");
            CreateIndex("dbo.PieceRateWageInfoes", "AlternateWageData_Id");
            CreateIndex("dbo.HourlyWageInfoes", "AlternateWageData_Id");
            AddForeignKey("dbo.PieceRateWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas", "Id");
            AddForeignKey("dbo.HourlyWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas", "Id");
        }
    }
}
