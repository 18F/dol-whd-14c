namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpolyerInfoUpdates : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EmployerInfoes", name: "Attachment_Id", newName: "SCAAttachment_Id");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_Attachment_Id", newName: "IX_SCAAttachment_Id");
            AddColumn("dbo.EmployerInfoes", "TradeName", c => c.String(nullable: false));
            AddColumn("dbo.EmployerInfoes", "PriorLegalName", c => c.String(nullable: false));
            AddColumn("dbo.EmployerInfoes", "ParentLegalName", c => c.String());
            AddColumn("dbo.EmployerInfoes", "ParentTradeName", c => c.String());
            AddColumn("dbo.EmployerInfoes", "SendMailToParent", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployerInfoes", "RepresentativePayee", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployerInfoes", "TakeCreditForCosts", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeOther", c => c.String());
            AddColumn("dbo.EmployerInfoes", "TemporaryAuthority", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployerInfoes", "ParentAddress_Id", c => c.Int());
            AddColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionType_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployerInfoes", "ParentAddress_Id");
            CreateIndex("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionType_Id");
            AddForeignKey("dbo.EmployerInfoes", "ParentAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionType_Id", "dbo.Responses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionType_Id", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoes", "ParentAddress_Id", "dbo.Addresses");
            DropIndex("dbo.EmployerInfoes", new[] { "ProvidingFacilitiesDeductionType_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "ParentAddress_Id" });
            DropColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionType_Id");
            DropColumn("dbo.EmployerInfoes", "ParentAddress_Id");
            DropColumn("dbo.EmployerInfoes", "TemporaryAuthority");
            DropColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeOther");
            DropColumn("dbo.EmployerInfoes", "TakeCreditForCosts");
            DropColumn("dbo.EmployerInfoes", "RepresentativePayee");
            DropColumn("dbo.EmployerInfoes", "SendMailToParent");
            DropColumn("dbo.EmployerInfoes", "ParentTradeName");
            DropColumn("dbo.EmployerInfoes", "ParentLegalName");
            DropColumn("dbo.EmployerInfoes", "PriorLegalName");
            DropColumn("dbo.EmployerInfoes", "TradeName");
            RenameIndex(table: "dbo.EmployerInfoes", name: "IX_SCAAttachment_Id", newName: "IX_Attachment_Id");
            RenameColumn(table: "dbo.EmployerInfoes", name: "SCAAttachment_Id", newName: "Attachment_Id");
        }
    }
}
