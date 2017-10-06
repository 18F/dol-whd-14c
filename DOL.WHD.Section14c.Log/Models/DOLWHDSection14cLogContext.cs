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
    
        public DOLWHDSection14cLogContext() : base("name=DOLWHDSection14cLogContext")
        {
        }

        public System.Data.Entity.DbSet<DOL.WHD.Section14c.Log.Models.APIActivityLogs> ActivityLogs { get; set; }
        public System.Data.Entity.DbSet<DOL.WHD.Section14c.Log.Models.APIErrorLogs> ErrorLogs { get; set; }
    }
}
