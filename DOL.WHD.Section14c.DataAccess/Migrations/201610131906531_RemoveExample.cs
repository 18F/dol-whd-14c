namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExample : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ExampleModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExampleModels",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Number);
            
        }
    }
}
