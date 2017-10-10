using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DOL.WHD.Section14c.Log.Models
{
    public class DOLWHDSection14cLogContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DOLWHDSection14cLogContext() : base("name=ApplicationLogContext")
        {
        }

        public System.Data.Entity.DbSet<DOL.WHD.Section14c.Log.Models.APIActivityLogs> ActivityLogs { get; set; }
        public System.Data.Entity.DbSet<DOL.WHD.Section14c.Log.Models.APIErrorLogs> ErrorLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder
            //  .Entity<APIActivityLogs>()
            //  .MapToStoredProcedures(s =>
            //     s.Insert(i => i.HasName("insert_activity_log")
            //                   .Parameter(b => b.Message, "@message")
            //                   .Parameter(b => b.Level, "@Level")
            //                   .Parameter(b => b.LogTime, "CURRENT TIMESTAMP")
            //                   .Parameter(b => b.User, "@logger")
            //                   .Parameter(b => b.Method, "@properties")
            //                   ));


        }

        
    }
}
