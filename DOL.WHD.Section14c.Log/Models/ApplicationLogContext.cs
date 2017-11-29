using DOL.WHD.Section14c.Log.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using DOL.WHD.Section14c.Log.Models;

namespace DOL.WHD.Section14c.Log.DataAccess
{
    /// <summary>
    /// Database context for logging
    /// </summary>
    public class ApplicationLogContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationLogContext() : base("name=ApplicationLogContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationLogContext, Configuration>());
        }

        /// <summary>
        /// Create a new application log context
        /// </summary>
        /// <returns>A new application log context</returns>
        public static ApplicationLogContext Create()
        {
            return new ApplicationLogContext();
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<APIActivityLogs> ActivityLogs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<APIErrorLogs> ErrorLogs { get; set; }

        /// <summary>
        /// Model-creating event handler
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
