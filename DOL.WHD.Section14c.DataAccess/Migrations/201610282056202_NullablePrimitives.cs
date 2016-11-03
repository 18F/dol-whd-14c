namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullablePrimitives : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationSubmissions", "NumEmployeesRepresentativePayee", c => c.Int());
            AlterColumn("dbo.EmployerInfoes", "SendMailToParent", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployerInfoes", "SendMailToParent", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ApplicationSubmissions", "NumEmployeesRepresentativePayee", c => c.Int(nullable: false));
        }
    }
}
