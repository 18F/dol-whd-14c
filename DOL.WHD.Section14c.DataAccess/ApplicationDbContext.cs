using System.Data.Entity;
using DOL.WHD.Section14c.DataAccess.Migrations;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web;
using System.Security.Claims;

namespace DOL.WHD.Section14c.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base(nameOrConnectionString: "ApplicationDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ApplicationSubmission> ApplicationSubmissions { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<ApplicationSave> ApplicationSaves { get; set; }

        public DbSet<Attachment> FileUploads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationSubmission>()
                .HasMany(s => s.EstablishmentType)
                .WithMany()
                .Map(m => m.ToTable("AppSubmissionEstablishmentType"));

            modelBuilder.Entity<ApplicationSubmission>()
                .HasMany(s => s.ProvidingFacilitiesDeductionType)
                .WithMany()
                .Map(m => m.ToTable("AppSubmissionFacilitiesDeductionType"));

            modelBuilder.Entity<WorkSite>()
                .HasMany(s => s.WorkSiteType)
                .WithMany()
                .Map(m => m.ToTable("WorkSiteWorkSiteType"));

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
    }
}
