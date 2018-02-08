namespace DOL.WHD.Section14c.DataAccess.MigrationsDB2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attachments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "EncryptionKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attachments", "EncryptionKey");
        }
    }
}
