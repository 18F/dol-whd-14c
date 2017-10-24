namespace DOL.WHD.Section14c.Log.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using DOL.WHD.Section14c.Log.DataAccess;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationLogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("Devart.Data.DB2", new Devart.Data.DB2.Entity.Migrations.DB2EntityMigrationSqlGenerator());
        }

        protected override void Seed(ApplicationLogContext context)
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
        }
    }
}
