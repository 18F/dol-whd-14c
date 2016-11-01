namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HasVerifiedDocumentationTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WIOAs", "HasVerifiedDocumentation", c => c.Boolean(nullable: false));
            DropColumn("dbo.WIOAs", "HasVerfiedDocumentaion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WIOAs", "HasVerfiedDocumentaion", c => c.Boolean(nullable: false));
            DropColumn("dbo.WIOAs", "HasVerifiedDocumentation");
        }
    }
}
