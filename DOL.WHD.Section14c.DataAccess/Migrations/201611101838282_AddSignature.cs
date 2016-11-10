namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSignature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Signatures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Agreement = c.Boolean(nullable: false),
                        FullName = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            AddColumn("dbo.ApplicationSubmissions", "Signature_Id", c => c.Guid());
            CreateIndex("dbo.ApplicationSubmissions", "Signature_Id");
            AddForeignKey("dbo.ApplicationSubmissions", "Signature_Id", "dbo.Signatures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSubmissions", "Signature_Id", "dbo.Signatures");
            DropForeignKey("dbo.Signatures", "LastModifiedBy_Id", "dbo.Users");
            DropIndex("dbo.Signatures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "Signature_Id" });
            DropColumn("dbo.ApplicationSubmissions", "Signature_Id");
            DropTable("dbo.Signatures");
        }
    }
}
