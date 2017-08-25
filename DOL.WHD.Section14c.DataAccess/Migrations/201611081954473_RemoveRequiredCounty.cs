namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredCounty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "County", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "County", c => c.String(nullable: false));
        }
    }
}
