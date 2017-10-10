namespace DOL.WHD.Section14c.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.APIActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 2147483647, unicode: false),
                        EIN = c.String(),
                        UserId = c.String(),
                        User = c.String(),
                        LogTime = c.String(),
                        Level = c.String(),
                        Exception = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.APIErrorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 2147483647, unicode: false),
                        EIN = c.String(),
                        UserId = c.String(),
                        User = c.String(),
                        LogTime = c.String(),
                        Level = c.String(),
                        Exception = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.APIErrorLogs");
            DropTable("dbo.APIActivityLogs");
        }
    }
}
