namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponsesAddShortDisplay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responses", "ShortDisplay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Responses", "ShortDisplay");
        }
    }
}
