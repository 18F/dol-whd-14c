namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationAdminFields : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ApplicationSubmissions", "CertificateNumber", "PreviousCertificateNumber");
            AddColumn("dbo.ApplicationSubmissions", "CertificateEffectiveDate", c => c.DateTime());
            AddColumn("dbo.ApplicationSubmissions", "CertificateExpirationDate", c => c.DateTime());
            AddColumn("dbo.ApplicationSubmissions", "CertificateNumber", c => c.String());
        }
        
        public override void Down()
        {
            RenameColumn("dbo.ApplicationSubmissions", "PreviousCertificateNumber", "CertificateNumber");
            DropColumn("dbo.ApplicationSubmissions", "CertificateExpirationDate");
            DropColumn("dbo.ApplicationSubmissions", "CertificateEffectiveDate");
            DropColumn("dbo.ApplicationSubmissions", "CertificateNumber");
        }
    }
}
