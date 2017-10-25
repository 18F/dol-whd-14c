namespace DOL.WHD.Section14c.DataAccess.MigrationsDB2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationSaves",
                c => new
                    {
                        EIN = c.String(nullable: false, maxLength: 128),
                        ApplicationState = c.String(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EIN)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastPasswordChangedDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrganizationMemberships",
                c => new
                    {
                        MembershipId = c.String(nullable: false, maxLength: 128),
                        EIN = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RoleFeatures",
                c => new
                    {
                        RoleFeatureId = c.Int(nullable: false, identity: true),
                        ApplicationRole_Id = c.String(maxLength: 128),
                        Feature_Id = c.Int(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RoleFeatureId)
                .ForeignKey("dbo.Roles", t => t.ApplicationRole_Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Features", t => t.Feature_Id, cascadeDelete: true)
                .Index(t => t.ApplicationRole_Id)
                .Index(t => t.Feature_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationSubmissions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EIN = c.String(nullable: false),
                        ApplicationTypeId = c.Int(nullable: false),
                        HasPreviousApplication = c.Boolean(nullable: false),
                        HasPreviousCertificate = c.Boolean(nullable: false),
                        PreviousCertificateNumber = c.String(),
                        ContactName = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        ContactFax = c.String(),
                        ContactEmail = c.String(nullable: false),
                        PayTypeId = c.Int(),
                        TotalNumWorkSites = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CertificateEffectiveDate = c.DateTime(),
                        CertificateExpirationDate = c.DateTime(),
                        CertificateNumber = c.String(),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        Employer_Id = c.String(nullable: false, maxLength: 128),
                        HourlyWageInfo_Id = c.String(maxLength: 128),
                        PieceRateWageInfo_Id = c.String(maxLength: 128),
                        Signature_Id = c.String(maxLength: 128),
                        WIOA_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Responses", t => t.ApplicationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.EmployerInfoes", t => t.Employer_Id, cascadeDelete: true)
                .ForeignKey("dbo.HourlyWageInfoes", t => t.HourlyWageInfo_Id)
                .ForeignKey("dbo.Responses", t => t.PayTypeId)
                .ForeignKey("dbo.PieceRateWageInfoes", t => t.PieceRateWageInfo_Id)
                .ForeignKey("dbo.Signatures", t => t.Signature_Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.WIOAs", t => t.WIOA_Id, cascadeDelete: true)
                .Index(t => t.ApplicationTypeId)
                .Index(t => t.PayTypeId)
                .Index(t => t.StatusId)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Employer_Id)
                .Index(t => t.HourlyWageInfo_Id)
                .Index(t => t.PieceRateWageInfo_Id)
                .Index(t => t.Signature_Id)
                .Index(t => t.WIOA_Id);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionKey = c.String(nullable: false),
                        Display = c.String(nullable: false),
                        SubDisplay = c.String(),
                        ShortDisplay = c.String(),
                        OtherValueKey = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.EmployerInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LegalName = c.String(nullable: false),
                        HasTradeName = c.Boolean(nullable: false),
                        TradeName = c.String(),
                        LegalNameHasChanged = c.Boolean(nullable: false),
                        PriorLegalName = c.String(),
                        HasMailingAddress = c.Boolean(),
                        HasParentOrg = c.Boolean(nullable: false),
                        ParentLegalName = c.String(),
                        ParentTradeName = c.String(),
                        SendMailToParent = c.Boolean(),
                        EmployerStatusId = c.Int(nullable: false),
                        EmployerStatusOther = c.String(),
                        IsEducationalAgency = c.Boolean(nullable: false),
                        FiscalQuarterEndDate = c.DateTime(),
                        PCA = c.Boolean(nullable: false),
                        SCAId = c.Int(nullable: false),
                        SCACount = c.Int(),
                        SCAAttachmentId = c.String(maxLength: 128),
                        EO13658Id = c.Int(nullable: false),
                        RepresentativePayee = c.Boolean(nullable: false),
                        TotalDisabledWorkers = c.Int(),
                        TakeCreditForCosts = c.Boolean(nullable: false),
                        TemporaryAuthority = c.Boolean(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        MailingAddress_Id = c.String(maxLength: 128),
                        NumSubminimalWageWorkers_Id = c.String(maxLength: 128),
                        ParentAddress_Id = c.String(maxLength: 128),
                        PhysicalAddress_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Responses", t => t.EmployerStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.EO13658Id, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.MailingAddress_Id)
                .ForeignKey("dbo.WorkerCountInfoes", t => t.NumSubminimalWageWorkers_Id)
                .ForeignKey("dbo.Addresses", t => t.ParentAddress_Id)
                .ForeignKey("dbo.Addresses", t => t.PhysicalAddress_Id, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.SCAId, cascadeDelete: true)
                .ForeignKey("dbo.Attachments", t => t.SCAAttachmentId)
                .Index(t => t.EmployerStatusId)
                .Index(t => t.SCAId)
                .Index(t => t.SCAAttachmentId)
                .Index(t => t.EO13658Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.MailingAddress_Id)
                .Index(t => t.NumSubminimalWageWorkers_Id)
                .Index(t => t.ParentAddress_Id)
                .Index(t => t.PhysicalAddress_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        County = c.String(),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.WorkerCountInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Total = c.Int(nullable: false),
                        WorkCenter = c.Int(nullable: false),
                        PatientWorkers = c.Int(nullable: false),
                        SWEP = c.Int(nullable: false),
                        BusinessEstablishment = c.Int(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.EmployerInfoFacilitiesDeductionType",
                c => new
                    {
                        EmployerInfoId = c.String(nullable: false, maxLength: 128),
                        ProvidingFacilitiesDeductionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployerInfoId, t.ProvidingFacilitiesDeductionTypeId })
                .ForeignKey("dbo.EmployerInfoes", t => t.EmployerInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.ProvidingFacilitiesDeductionTypeId, cascadeDelete: true)
                .Index(t => t.EmployerInfoId)
                .Index(t => t.ProvidingFacilitiesDeductionTypeId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OriginalFileName = c.String(nullable: false, maxLength: 255),
                        RepositoryFilePath = c.String(nullable: false, maxLength: 255),
                        FileSize = c.Long(nullable: false),
                        MimeType = c.String(nullable: false, maxLength: 255),
                        EIN = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.AppSubmissionEstablishmentType",
                c => new
                    {
                        ApplicationSubmissionId = c.String(nullable: false, maxLength: 128),
                        EstablishmentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationSubmissionId, t.EstablishmentTypeId })
                .ForeignKey("dbo.ApplicationSubmissions", t => t.ApplicationSubmissionId, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.EstablishmentTypeId, cascadeDelete: true)
                .Index(t => t.ApplicationSubmissionId)
                .Index(t => t.EstablishmentTypeId);
            
            CreateTable(
                "dbo.HourlyWageInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        WorkMeasurementFrequency = c.String(nullable: false),
                        NumWorkers = c.Int(nullable: false),
                        JobName = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        PrevailingWageMethodId = c.Int(nullable: false),
                        SCAWageDeterminationAttachmentId = c.String(maxLength: 128),
                        AttachmentId = c.String(nullable: false, maxLength: 128),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        AlternateWageData_Id = c.String(maxLength: 128),
                        MostRecentPrevailingWageSurvey_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlternateWageDatas", t => t.AlternateWageData_Id)
                .ForeignKey("dbo.Attachments", t => t.AttachmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.PrevailingWageSurveyInfoes", t => t.MostRecentPrevailingWageSurvey_Id)
                .ForeignKey("dbo.Responses", t => t.PrevailingWageMethodId, cascadeDelete: true)
                .ForeignKey("dbo.Attachments", t => t.SCAWageDeterminationAttachmentId)
                .Index(t => t.PrevailingWageMethodId)
                .Index(t => t.SCAWageDeterminationAttachmentId)
                .Index(t => t.AttachmentId)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.AlternateWageData_Id)
                .Index(t => t.MostRecentPrevailingWageSurvey_Id);
            
            CreateTable(
                "dbo.AlternateWageDatas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AlternateWorkDescription = c.String(nullable: false),
                        AlternateDataSourceUsed = c.String(nullable: false),
                        PrevailingWageProvidedBySource = c.Double(nullable: false),
                        DataRetrieved = c.DateTime(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PrevailingWageSurveyInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PrevailingWageDetermined = c.Double(nullable: false),
                        AttachmentId = c.String(maxLength: 128),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attachments", t => t.AttachmentId)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.AttachmentId)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.SourceEmployers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EmployerName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        ContactName = c.String(nullable: false),
                        ContactTitle = c.String(nullable: false),
                        ContactDate = c.DateTime(nullable: false),
                        JobDescription = c.String(nullable: false),
                        ExperiencedWorkerWageProvided = c.String(nullable: false),
                        ConclusionWageRateNotBasedOnEntry = c.String(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        Address_Id = c.String(nullable: false, maxLength: 128),
                        PrevailingWageSurveyInfo_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.PrevailingWageSurveyInfoes", t => t.PrevailingWageSurveyInfo_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.PrevailingWageSurveyInfo_Id);
            
            CreateTable(
                "dbo.PieceRateWageInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PieceRateWorkDescription = c.String(nullable: false),
                        PrevailingWageDeterminedForJob = c.Double(nullable: false),
                        StandardProductivity = c.Double(nullable: false),
                        PieceRatePaidToWorkers = c.Double(nullable: false),
                        NumWorkers = c.Int(nullable: false),
                        JobName = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        PrevailingWageMethodId = c.Int(nullable: false),
                        SCAWageDeterminationAttachmentId = c.String(maxLength: 128),
                        AttachmentId = c.String(nullable: false, maxLength: 128),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        AlternateWageData_Id = c.String(maxLength: 128),
                        MostRecentPrevailingWageSurvey_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlternateWageDatas", t => t.AlternateWageData_Id)
                .ForeignKey("dbo.Attachments", t => t.AttachmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.PrevailingWageSurveyInfoes", t => t.MostRecentPrevailingWageSurvey_Id)
                .ForeignKey("dbo.Responses", t => t.PrevailingWageMethodId, cascadeDelete: true)
                .ForeignKey("dbo.Attachments", t => t.SCAWageDeterminationAttachmentId)
                .Index(t => t.PrevailingWageMethodId)
                .Index(t => t.SCAWageDeterminationAttachmentId)
                .Index(t => t.AttachmentId)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.AlternateWageData_Id)
                .Index(t => t.MostRecentPrevailingWageSurvey_Id);
            
            CreateTable(
                "dbo.Signatures",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Agreement = c.Boolean(nullable: false),
                        FullName = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.WIOAs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HasVerifiedDocumentation = c.Boolean(nullable: false),
                        HasWIOAWorkers = c.Boolean(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.WIOAWorkers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false),
                        WIOAWorkerVerifiedId = c.Int(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        WIOA_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Responses", t => t.WIOAWorkerVerifiedId, cascadeDelete: true)
                .ForeignKey("dbo.WIOAs", t => t.WIOA_Id)
                .Index(t => t.WIOAWorkerVerifiedId)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.WIOA_Id);
            
            CreateTable(
                "dbo.WorkSites",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        WorkSiteTypeId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        SCA = c.Boolean(nullable: false),
                        FederalContractWorkPerformed = c.Boolean(nullable: false),
                        NumEmployees = c.Int(),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        Address_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationSubmission_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Responses", t => t.WorkSiteTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationSubmissions", t => t.ApplicationSubmission_Id)
                .Index(t => t.WorkSiteTypeId)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.ApplicationSubmission_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        PrimaryDisabilityId = c.Int(nullable: false),
                        PrimaryDisabilityOther = c.String(),
                        WorkType = c.String(nullable: false),
                        NumJobs = c.Int(nullable: false),
                        AvgWeeklyHours = c.Double(nullable: false),
                        AvgHourlyEarnings = c.Double(nullable: false),
                        PrevailingWage = c.Double(nullable: false),
                        ProductivityMeasure = c.Double(),
                        CommensurateWageRate = c.String(nullable: false),
                        TotalHours = c.Double(nullable: false),
                        WorkAtOtherSite = c.Boolean(nullable: false),
                        PriviteId = c.String(),
                        CreatedBy_Id = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        LastModifiedAt = c.DateTime(nullable: false),
                        WorkSite_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Responses", t => t.PrimaryDisabilityId, cascadeDelete: true)
                .ForeignKey("dbo.WorkSites", t => t.WorkSite_Id)
                .Index(t => t.PrimaryDisabilityId)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.WorkSite_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkSites", "ApplicationSubmission_Id", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.WorkSites", "WorkSiteTypeId", "dbo.Responses");
            DropForeignKey("dbo.Employees", "WorkSite_Id", "dbo.WorkSites");
            DropForeignKey("dbo.Employees", "PrimaryDisabilityId", "dbo.Responses");
            DropForeignKey("dbo.Employees", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.WorkSites", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.WorkSites", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.ApplicationSubmissions", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.WIOAWorkers", "WIOA_Id", "dbo.WIOAs");
            DropForeignKey("dbo.WIOAWorkers", "WIOAWorkerVerifiedId", "dbo.Responses");
            DropForeignKey("dbo.WIOAWorkers", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.WIOAs", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ApplicationSubmissions", "StatusId", "dbo.Status");
            DropForeignKey("dbo.ApplicationSubmissions", "Signature_Id", "dbo.Signatures");
            DropForeignKey("dbo.Signatures", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ApplicationSubmissions", "PieceRateWageInfo_Id", "dbo.PieceRateWageInfoes");
            DropForeignKey("dbo.PieceRateWageInfoes", "SCAWageDeterminationAttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.PieceRateWageInfoes", "PrevailingWageMethodId", "dbo.Responses");
            DropForeignKey("dbo.PieceRateWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.PieceRateWageInfoes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PieceRateWageInfoes", "AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.PieceRateWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropForeignKey("dbo.ApplicationSubmissions", "PayTypeId", "dbo.Responses");
            DropForeignKey("dbo.ApplicationSubmissions", "HourlyWageInfo_Id", "dbo.HourlyWageInfoes");
            DropForeignKey("dbo.HourlyWageInfoes", "SCAWageDeterminationAttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.HourlyWageInfoes", "PrevailingWageMethodId", "dbo.Responses");
            DropForeignKey("dbo.HourlyWageInfoes", "MostRecentPrevailingWageSurvey_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.SourceEmployers", "PrevailingWageSurveyInfo_Id", "dbo.PrevailingWageSurveyInfoes");
            DropForeignKey("dbo.SourceEmployers", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.SourceEmployers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.PrevailingWageSurveyInfoes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PrevailingWageSurveyInfoes", "AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.HourlyWageInfoes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.HourlyWageInfoes", "AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.HourlyWageInfoes", "AlternateWageData_Id", "dbo.AlternateWageDatas");
            DropForeignKey("dbo.AlternateWageDatas", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "EstablishmentTypeId", "dbo.Responses");
            DropForeignKey("dbo.AppSubmissionEstablishmentType", "ApplicationSubmissionId", "dbo.ApplicationSubmissions");
            DropForeignKey("dbo.ApplicationSubmissions", "Employer_Id", "dbo.EmployerInfoes");
            DropForeignKey("dbo.EmployerInfoes", "SCAAttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.Attachments", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.EmployerInfoes", "SCAId", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoFacilitiesDeductionType", "ProvidingFacilitiesDeductionTypeId", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoFacilitiesDeductionType", "EmployerInfoId", "dbo.EmployerInfoes");
            DropForeignKey("dbo.EmployerInfoes", "PhysicalAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.EmployerInfoes", "ParentAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.EmployerInfoes", "NumSubminimalWageWorkers_Id", "dbo.WorkerCountInfoes");
            DropForeignKey("dbo.WorkerCountInfoes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.EmployerInfoes", "MailingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.EmployerInfoes", "EO13658Id", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoes", "EmployerStatusId", "dbo.Responses");
            DropForeignKey("dbo.EmployerInfoes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ApplicationSubmissions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ApplicationSubmissions", "ApplicationTypeId", "dbo.Responses");
            DropForeignKey("dbo.Responses", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ApplicationSaves", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleFeatures", "Feature_Id", "dbo.Features");
            DropForeignKey("dbo.RoleFeatures", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.RoleFeatures", "ApplicationRole_Id", "dbo.Roles");
            DropForeignKey("dbo.OrganizationMemberships", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.OrganizationMemberships", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "WorkSite_Id" });
            DropIndex("dbo.Employees", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Employees", new[] { "PrimaryDisabilityId" });
            DropIndex("dbo.WorkSites", new[] { "ApplicationSubmission_Id" });
            DropIndex("dbo.WorkSites", new[] { "Address_Id" });
            DropIndex("dbo.WorkSites", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.WorkSites", new[] { "WorkSiteTypeId" });
            DropIndex("dbo.WIOAWorkers", new[] { "WIOA_Id" });
            DropIndex("dbo.WIOAWorkers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.WIOAWorkers", new[] { "WIOAWorkerVerifiedId" });
            DropIndex("dbo.WIOAs", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Signatures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "AlternateWageData_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "AttachmentId" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "SCAWageDeterminationAttachmentId" });
            DropIndex("dbo.PieceRateWageInfoes", new[] { "PrevailingWageMethodId" });
            DropIndex("dbo.SourceEmployers", new[] { "PrevailingWageSurveyInfo_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "Address_Id" });
            DropIndex("dbo.SourceEmployers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PrevailingWageSurveyInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PrevailingWageSurveyInfoes", new[] { "AttachmentId" });
            DropIndex("dbo.AlternateWageDatas", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "MostRecentPrevailingWageSurvey_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "AlternateWageData_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "AttachmentId" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "SCAWageDeterminationAttachmentId" });
            DropIndex("dbo.HourlyWageInfoes", new[] { "PrevailingWageMethodId" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "EstablishmentTypeId" });
            DropIndex("dbo.AppSubmissionEstablishmentType", new[] { "ApplicationSubmissionId" });
            DropIndex("dbo.Attachments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.EmployerInfoFacilitiesDeductionType", new[] { "ProvidingFacilitiesDeductionTypeId" });
            DropIndex("dbo.EmployerInfoFacilitiesDeductionType", new[] { "EmployerInfoId" });
            DropIndex("dbo.WorkerCountInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Addresses", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "PhysicalAddress_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "ParentAddress_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "NumSubminimalWageWorkers_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "MailingAddress_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "EO13658Id" });
            DropIndex("dbo.EmployerInfoes", new[] { "SCAAttachmentId" });
            DropIndex("dbo.EmployerInfoes", new[] { "SCAId" });
            DropIndex("dbo.EmployerInfoes", new[] { "EmployerStatusId" });
            DropIndex("dbo.Responses", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "WIOA_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "Signature_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "PieceRateWageInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "HourlyWageInfo_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "Employer_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "StatusId" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "PayTypeId" });
            DropIndex("dbo.ApplicationSubmissions", new[] { "ApplicationTypeId" });
            DropIndex("dbo.RoleFeatures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.RoleFeatures", new[] { "Feature_Id" });
            DropIndex("dbo.RoleFeatures", new[] { "ApplicationRole_Id" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.OrganizationMemberships", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.OrganizationMemberships", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.ApplicationSaves", new[] { "LastModifiedBy_Id" });
            DropTable("dbo.Employees");
            DropTable("dbo.WorkSites");
            DropTable("dbo.WIOAWorkers");
            DropTable("dbo.WIOAs");
            DropTable("dbo.Signatures");
            DropTable("dbo.PieceRateWageInfoes");
            DropTable("dbo.SourceEmployers");
            DropTable("dbo.PrevailingWageSurveyInfoes");
            DropTable("dbo.AlternateWageDatas");
            DropTable("dbo.HourlyWageInfoes");
            DropTable("dbo.AppSubmissionEstablishmentType");
            DropTable("dbo.Attachments");
            DropTable("dbo.EmployerInfoFacilitiesDeductionType");
            DropTable("dbo.WorkerCountInfoes");
            DropTable("dbo.Addresses");
            DropTable("dbo.EmployerInfoes");
            DropTable("dbo.Responses");
            DropTable("dbo.ApplicationSubmissions");
            DropTable("dbo.Status");
            DropTable("dbo.Features");
            DropTable("dbo.RoleFeatures");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.OrganizationMemberships");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.ApplicationSaves");
        }
    }
}
