namespace DOL.WHD.Section14c.DataAccess.MigrationsDB2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttachmentSupportedFileTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttachmentSupportedFileTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AttachmentSupportedFileTypes");
        }
    }
}
