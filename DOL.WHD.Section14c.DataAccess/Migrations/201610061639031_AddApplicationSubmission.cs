namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationSubmission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationSubmissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        ApplicationType = c.String(nullable: false),
                        HasPreviousApplication = c.Boolean(nullable: false),
                        HasPreviousCertificate = c.Boolean(nullable: false),
                        CertificateNumber = c.String(),
                        TemporaryAuthority = c.Boolean(nullable: false),
                        ContactName = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        ContactFax = c.String(),
                        ContactEmail = c.String(nullable: false),
                        PayType = c.String(nullable: false),
                        TotalNumWorkSites = c.Int(nullable: false),
                        RepresentativePayeeSocialSecurityBenefits = c.Boolean(nullable: false),
                        NumEmployeesRepresentativePayee = c.Int(nullable: false),
                        ProvidingFacilities = c.Boolean(nullable: false),
                        ProvidingFacilitiesDeductionType = c.String(),
                        ReviewedDocumentation = c.Boolean(nullable: false),
                        Employer_Id = c.Int(nullable: false),
                        WageTypeInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployerInfoes", t => t.Employer_Id, cascadeDelete: true)
                .ForeignKey("dbo.WageTypeInfoes", t => t.WageTypeInfo_Id)
                .Index(t => t.Employer_Id)
                .Index(t => t.WageTypeInfo_Id);
            
            CreateTable(
                "dbo.EmployerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LegalName = c.String(nullable: false),
                        HasTradeName = c.Boolean(nullable: false),
                        LegalNameHasChanged = c.Boolean(nullable: false),
                        HasParentOrg = c.Boolean(nullable: false),
                        Status = c.String(nullable: false),
                        IsEducationalAgency = c.Boolean(nullable: false),
                        FiscalQuarterEndDate = c.DateTime(nullable: false),
                        PCA = c.Boolean(nullable: false),
                        SCA = c.Int(nullable: false),
                        EO13658 = c.Int(nullable: false),
                        NumSubminimalWageWorkers_Id = c.Int(nullable: false),
                        PhysicalAddress_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkerCountInfoes", t => t.NumSubminimalWageWorkers_Id, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.PhysicalAddress_Id, cascadeDelete: true)
                .Index(t => t.NumSubminimalWageWorkers_Id)
                .Index(t => t.PhysicalAddress_Id);
            
            CreateTable(
                "dbo.WorkerCountInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Int(nullable: false),
                        WorkCenter = c.Int(nullable: false),
                        PatientWorkers = c.Int(nullable: false),
                        SWEP = c.Int(nullable: false),
                        BusinessEstablishment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        County = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WageTypeInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumWorkers = c.Int(nullable: false),
                        JobName = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        PrevailingWageMethod = c.String(nullable: false),
                        FrequencyOfWorkMeasurements = c.String(),
                        NumPieceRateWorkers = c.Int(),
                        WorkDescription = c.String(),
                        WageRatePerHour = c.Double(),
                        ProductivityUnitsPerHour = c.Double(),
                        PieceRatePerUnit = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        MostRecentPrevailingWageSurvey_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrevailingWageSurveyInfoes", t => t.MostRecentPrevailingWageSurvey_Id)
                .Index(t => t.MostRecentPrevailingWageSurvey_Id);
            
            CreateTable(
                "dbo.PrevailingWageSurveyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BasedOnSurvey = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSubmissions", "WageTypeInfo_Id", "dbo.WageTypeInfoes");
            DropForeignKey("dbo.WageTypeInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.ApplicationSubmissions", "Employer_Id", "dbo.EmployerInfoes");
            DropForeignKey("dbo.EmployerInfoes", "PhysicalAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes");
            DropIndex("dbo.WageTypeInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "PhysicalAddress_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "NumSubminimalWageWorkers_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "WageTypeInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "Employer_Id" });
            DropTable("dbo.PrevailingWageSurveyInfoes");
            DropTable("dbo.WageTypeInfoes");
            DropTable("dbo.Addresses");
            DropTable("dbo.WorkerCountInfoes");
            DropTable("dbo.EmployerInfoes");
            DropTable("dbo.ApplicationSubmissions");
        }
    }
}
