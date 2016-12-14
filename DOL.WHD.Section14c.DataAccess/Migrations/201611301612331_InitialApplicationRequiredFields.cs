namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialApplicationRequiredFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationSubmissions", "PayTypeId", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes");
            DropIndex("dbo.ApplicationSubmissions", new[] { "PayTypeId" });
            DropIndex("dbo.EmployerInfoes", new[] { "NumSubminimalWageWorkers_Id" });
            AlterColumn("dbo.ApplicationSubmissions", "PayTypeId", c => c.Int());
            AlterColumn("dbo.EmployerInfoes", "FiscalQuarterEndDate", c => c.DateTime());
            AlterColumn("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", c => c.Guid());
            AlterColumn("dbo.WorkSites", "NumEmployees", c => c.Int());
            CreateIndex("dbo.ApplicationSubmissions", "PayTypeId");
            CreateIndex("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id");
            AddForeignKey("dbo.ApplicationSubmissions", "PayTypeId", "dbo.Responses", "Id");
            AddForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes");
            DropForeignKey("dbo.ApplicationSubmissions", "PayTypeId", "dbo.Responses");
            DropIndex("dbo.EmployerInfoes", new[] { "NumSubminimalWageWorkers_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "PayTypeId" });
            AlterColumn("dbo.WorkSites", "NumEmployees", c => c.Int(nullable: false));
            AlterColumn("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.EmployerInfoes", "FiscalQuarterEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ApplicationSubmissions", "PayTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id");
            CreateIndex("dbo.ApplicationSubmissions", "PayTypeId");
            AddForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationSubmissions", "PayTypeId", "dbo.Responses", "Id", cascadeDelete: true);
        }
    }
}
