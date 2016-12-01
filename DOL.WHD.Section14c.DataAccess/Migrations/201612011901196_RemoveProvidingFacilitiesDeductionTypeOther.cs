namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProvidingFacilitiesDeductionTypeOther : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeOther");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployerInfoes", "ProvidingFacilitiesDeductionTypeOther", c => c.String());
        }
    }
}
