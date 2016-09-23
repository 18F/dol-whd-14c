using System.Data.Entity;
using DOL.DWH.Section14c.DataAccess.Migrations;
using DOL.DWH.Section14c.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DOL.DWH.Section14c.DataAccess
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
    }
}
