using System.Data.Entity;
using DOL.WHD.Section14c.DataAccess.Migrations;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.AspNet.Identity.EntityFramework;

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

        public DbSet<ExampleModel> Numbers { get; set; }

        public DbSet<ApplicationSubmission> ApplicationSubmissions { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<ApplicationSave> ApplicationSaves { get; set; }

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

            modelBuilder.Entity<ApplicationSave>()
                .HasKey(s => new {s.UserId, s.EIN});
        }
    }
}
