namespace DOL.WHD.Section14c.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// Initial database migration
    /// </summary>
    public partial class Initial : DbMigration
    {
        /// <summary>
        /// Migration upgrade
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.APIActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CorrelationId = c.String(),                        
                        Message = c.String(nullable: false, maxLength: 2147483647, unicode: false),
                        EIN = c.String(),
                        UserId = c.String(),
                        User = c.String(),
                        Level = c.String(),
                        Exception = c.String(),
                        LogTime = c.String(),
                        StackTrace = c.String(),
                        IsServiceSideLog = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.APIErrorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CorrelationId = c.String(),                        
                        Message = c.String(nullable: false, maxLength: 2147483647, unicode: false),
                        EIN = c.String(),
                        UserId = c.String(),
                        User = c.String(),
                        Level = c.String(),
                        Exception = c.String(),
                        LogTime = c.String(),
                        StackTrace = c.String(),
                        IsServiceSideLog = c.Boolean(nullable: true, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        /// <summary>
        /// Migration downgrade
        /// </summary>
        public override void Down()
        {
            DropTable("dbo.APIErrorLogs");
            DropTable("dbo.APIActivityLogs");
        }
    }
}
