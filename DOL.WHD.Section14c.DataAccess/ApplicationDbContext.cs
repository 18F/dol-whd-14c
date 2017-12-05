using System.Data.Entity;
using DOL.WHD.Section14c.DataAccess.MigrationsDB2;
using DOL.WHD.Section14c.DataAccess.Migrations;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web;
using DOL.WHD.Section14c.Domain.Models.Identity;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base(nameOrConnectionString: "ApplicationDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ConfigurationDB2>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ApplicationSubmission> ApplicationSubmissions { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<ApplicationSave> ApplicationSaves { get; set; }

        public DbSet<Attachment> FileUploads { get; set; }

        public DbSet<RoleFeature> RoleFeatures { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Status> ApplicationStatuses { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<OrganizationMembership> OrganizationMemberships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // data constraints
            // Address
            modelBuilder.Entity<Address>().Property(a => a.StreetAddress).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.City).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.State).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.ZipCode).IsRequired();
            // AlternateWageData
            modelBuilder.Entity<AlternateWageData>().Property(a => a.AlternateWorkDescription).IsRequired();
            modelBuilder.Entity<AlternateWageData>().Property(a => a.AlternateDataSourceUsed).IsRequired();
            modelBuilder.Entity<AlternateWageData>().Property(a => a.PrevailingWageProvidedBySource).IsRequired();
            modelBuilder.Entity<AlternateWageData>().Property(a => a.DataRetrieved).IsRequired();
            // ApplicationSubmission
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.EIN).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.ApplicationTypeId).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.HasPreviousApplication).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.HasPreviousCertificate).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.ContactName).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.ContactPhone).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.ContactEmail).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().Property(a => a.TotalNumWorkSites).IsRequired();
            modelBuilder.Entity<ApplicationSubmission>().HasRequired(a => a.Employer);
            modelBuilder.Entity<ApplicationSubmission>().HasRequired(a => a.WIOA);
            modelBuilder.Entity<ApplicationSubmission>().HasRequired(a => a.Status);
            // Attachment
            modelBuilder.Entity<Attachment>().Property(a => a.OriginalFileName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Attachment>().Property(a => a.RepositoryFilePath).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Attachment>().Property(a => a.FileSize).IsRequired();
            modelBuilder.Entity<Attachment>().Property(a => a.MimeType).IsRequired().HasMaxLength(255);
            // Employee
            modelBuilder.Entity<Employee>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.PrimaryDisabilityId).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.WorkType).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.NumJobs).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.AvgWeeklyHours).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.AvgHourlyEarnings).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.PrevailingWage).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.CommensurateWageRate).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.TotalHours).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.WorkAtOtherSite).IsRequired();
            // EmployerInfo
            modelBuilder.Entity<EmployerInfo>().Property(a => a.LegalName).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.HasTradeName).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.LegalNameHasChanged).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.HasParentOrg).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.EmployerStatusId).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.IsEducationalAgency).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.PCA).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.SCAId).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.EO13658Id).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.RepresentativePayee).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.TakeCreditForCosts).IsRequired();
            modelBuilder.Entity<EmployerInfo>().Property(a => a.TemporaryAuthority).IsRequired();
            modelBuilder.Entity<EmployerInfo>().HasRequired(a => a.PhysicalAddress);
            // HourlyWageInfo
            modelBuilder.Entity<HourlyWageInfo>().Property(a => a.WorkMeasurementFrequency).IsRequired();
            modelBuilder.Entity<HourlyWageInfo>().Property(a => a.NumWorkers).IsRequired();
            modelBuilder.Entity<HourlyWageInfo>().Property(a => a.JobName).IsRequired();
            modelBuilder.Entity<HourlyWageInfo>().Property(a => a.JobDescription).IsRequired();
            modelBuilder.Entity<HourlyWageInfo>().Property(a => a.PrevailingWageMethodId).IsRequired();
            modelBuilder.Entity<HourlyWageInfo>().Property(a => a.AttachmentId).IsRequired();
            // PieceRateWageInfo
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.PieceRateWorkDescription).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.PrevailingWageDeterminedForJob).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.StandardProductivity).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.PieceRatePaidToWorkers).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.NumWorkers).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.JobName).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.JobDescription).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.PrevailingWageMethodId).IsRequired();
            modelBuilder.Entity<PieceRateWageInfo>().Property(a => a.AttachmentId).IsRequired();
            // PrevailingWageSurveyInfo
            modelBuilder.Entity<PrevailingWageSurveyInfo>().Property(a => a.PrevailingWageDetermined).IsRequired();
            // Response
            modelBuilder.Entity<Response>().Property(a => a.QuestionKey).IsRequired();
            modelBuilder.Entity<Response>().Property(a => a.Display).IsRequired();
            modelBuilder.Entity<Response>().Property(a => a.IsActive).IsRequired();
            // Signature
            modelBuilder.Entity<Signature>().Property(a => a.Agreement).IsRequired();
            modelBuilder.Entity<Signature>().Property(a => a.FullName).IsRequired();
            modelBuilder.Entity<Signature>().Property(a => a.Title).IsRequired();
            modelBuilder.Entity<Signature>().Property(a => a.Date).IsRequired();
            // SourceEmployer
            modelBuilder.Entity<SourceEmployer>().Property(a => a.EmployerName).IsRequired();
            modelBuilder.Entity<SourceEmployer>().Property(a => a.Phone).IsRequired();
            modelBuilder.Entity<SourceEmployer>().Property(a => a.ContactName).IsRequired();
            modelBuilder.Entity<SourceEmployer>().Property(a => a.ContactTitle).IsRequired();
            modelBuilder.Entity<SourceEmployer>().Property(a => a.ContactDate).IsRequired();
            modelBuilder.Entity<SourceEmployer>().Property(a => a.JobDescription).IsRequired();
            modelBuilder.Entity<SourceEmployer>().Property(a => a.ExperiencedWorkerWageProvided).IsRequired();
            modelBuilder.Entity<SourceEmployer>().Property(a => a.ConclusionWageRateNotBasedOnEntry).IsRequired();
            modelBuilder.Entity<SourceEmployer>().HasRequired(a => a.Address);
            // Status
            modelBuilder.Entity<Status>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Status>().Property(a => a.IsActive).IsRequired();
            // WIOA
            modelBuilder.Entity<WIOA>().Property(a => a.HasVerifiedDocumentation).IsRequired();
            modelBuilder.Entity<WIOA>().Property(a => a.HasWIOAWorkers).IsRequired();
            // WIOAWorker
            modelBuilder.Entity<WIOAWorker>().Property(a => a.FullName).IsRequired();
            modelBuilder.Entity<WIOAWorker>().Property(a => a.WIOAWorkerVerifiedId).IsRequired();
            // WorkerCountInfo
            modelBuilder.Entity<WorkerCountInfo>().Property(a => a.Total).IsRequired();
            modelBuilder.Entity<WorkerCountInfo>().Property(a => a.WorkCenter).IsRequired();
            modelBuilder.Entity<WorkerCountInfo>().Property(a => a.PatientWorkers).IsRequired();
            modelBuilder.Entity<WorkerCountInfo>().Property(a => a.SWEP).IsRequired();
            modelBuilder.Entity<WorkerCountInfo>().Property(a => a.BusinessEstablishment).IsRequired();
            // WorkSite
            modelBuilder.Entity<WorkSite>().Property(a => a.WorkSiteTypeId).IsRequired();
            modelBuilder.Entity<WorkSite>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<WorkSite>().Property(a => a.SCA).IsRequired();
            modelBuilder.Entity<WorkSite>().Property(a => a.FederalContractWorkPerformed).IsRequired();
            modelBuilder.Entity<WorkSite>().HasRequired(a => a.Address);

            // many to many relationships
            modelBuilder.Entity<ApplicationSubmissionEstablishmentType>()
                .ToTable("AppSubmissionEstablishmentType")
                .HasKey(k => new {k.ApplicationSubmissionId, k.EstablishmentTypeId});

            modelBuilder.Entity<EmployerInfoProvidingFacilitiesDeductionType>()
                .ToTable("EmployerInfoFacilitiesDeductionType")
                .HasKey(k => new { k.EmployerInfoId, k.ProvidingFacilitiesDeductionTypeId });

            modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.CreatedBy).WithMany();
            modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.LastModifiedBy).WithMany();
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");

        }

        public override int SaveChanges()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.UtcNow;
            var zeroTime = new DateTime();

            var userId = Guid.Empty.ToString();

            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                userId = HttpContext.Current.User.Identity.GetUserId();
            }

            foreach (var added in addedAuditedEntities)
            {
                if (added.CreatedAt == zeroTime) { added.CreatedAt = now; }
                added.LastModifiedAt = now;
                added.CreatedBy_Id = userId;
                added.LastModifiedBy_Id = userId;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                if (modified.CreatedAt == zeroTime)
                {
                    modified.CreatedAt = now;
                    modified.CreatedBy_Id = userId;
                }
                modified.LastModifiedAt = now;
                modified.LastModifiedBy_Id = userId;
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            var now = DateTime.UtcNow;
            var zeroTime = new DateTime();

            var userId = Guid.Empty.ToString();

            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                userId = HttpContext.Current.User.Identity.GetUserId();
            }

            foreach (var added in addedAuditedEntities)
            {
                if (added.CreatedAt == zeroTime) { added.CreatedAt = now; }
                added.LastModifiedAt = now;
                if (userId != Guid.Empty.ToString())
                {
                    added.CreatedBy_Id = userId;
                    added.LastModifiedBy_Id = userId;
                }
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                if (modified.CreatedAt == zeroTime)
                {
                    modified.CreatedAt = now;
                    if (userId != Guid.Empty.ToString())
                        modified.CreatedBy_Id = userId;
                }
                modified.LastModifiedAt = now;
                if (userId != Guid.Empty.ToString())
                    modified.LastModifiedBy_Id = userId;
            }

            return await base.SaveChangesAsync();
        }
    }
}
