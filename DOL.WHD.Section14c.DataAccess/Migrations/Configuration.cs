using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DOL.WHD.Section14c.DataAccess.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DOL.WHD.Section14c.DataAccess.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Numbers.AddOrUpdate(new ExampleModel() { Number = 1 });
            context.Numbers.AddOrUpdate(new ExampleModel() { Number = 2 });
            context.Numbers.AddOrUpdate(new ExampleModel() { Number = 3 });

        }
    }
}
