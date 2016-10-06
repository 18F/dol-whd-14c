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
    }
}
