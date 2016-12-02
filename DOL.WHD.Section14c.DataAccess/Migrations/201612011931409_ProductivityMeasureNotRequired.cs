namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductivityMeasureNotRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployerInfoes", "MailingAddress_Id", "dbo.Addresses");
            DropIndex("dbo.EmployerInfoes", new[] { "MailingAddress_Id" });
            AddColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeOther", c => c.String());
            AlterColumn("dbo.Employees", "ProductivityMeasure", c => c.Double());
            DropColumn("dbo.EmployerInfoes", "HasMailingAddress");
            DropColumn("dbo.EmployerInfoes", "MailingAddress_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployerInfoes", "MailingAddress_Id", c => c.Guid());
            AddColumn("dbo.EmployerInfoes", "HasMailingAddress", c => c.Boolean());
            AlterColumn("dbo.Employees", "ProductivityMeasure", c => c.Double(nullable: false));
            DropColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeOther");
            CreateIndex("dbo.EmployerInfoes", "MailingAddress_Id");
            AddForeignKey("dbo.EmployerInfoes", "MailingAddress_Id", "dbo.Addresses", "Id");
        }
    }
}
