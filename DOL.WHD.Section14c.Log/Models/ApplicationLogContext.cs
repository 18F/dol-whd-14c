using DOL.WHD.Section14c.Log.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using DOL.WHD.Section14c.Log.DataAccess.Models;

namespace DOL.WHD.Section14c.Log.DataAccess
{
    public class ApplicationLogContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ApplicationLogContext() : base("name=ApplicationLogContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationLogContext, Configuration>());
        }

        public static ApplicationLogContext Create()
        {
            return new ApplicationLogContext();
        }

        public System.Data.Entity.DbSet<APIActivityLogs> ActivityLogs { get; set; }
        public System.Data.Entity.DbSet<APIErrorLogs> ErrorLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
