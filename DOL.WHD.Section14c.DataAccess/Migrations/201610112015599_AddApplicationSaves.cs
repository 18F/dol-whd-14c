namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationSaves : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationSaves",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        EIN = c.String(nullable: false, maxLength: 128),
                        ApplicationState = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.EIN });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationSaves");
        }
    }
}
