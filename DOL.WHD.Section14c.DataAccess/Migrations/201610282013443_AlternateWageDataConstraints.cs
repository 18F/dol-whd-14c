namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlternateWageDataConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AlternateWageDatas", "AlternateWorkDescription", c => c.String(nullable: false));
            AlterColumn("dbo.AlternateWageDatas", "AlternateDataSourceUsed", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AlternateWageDatas", "AlternateDataSourceUsed", c => c.String());
            AlterColumn("dbo.AlternateWageDatas", "AlternateWorkDescription", c => c.String());
        }
    }
}
