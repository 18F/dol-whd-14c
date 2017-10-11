using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace DOL.WHD.Section14c.DataAccess
{
    // We must add this class only to change the migration history table name
    // We need to do it because the default migration history table name is __MigrationHistory and
    //     1. DB2 .net entity framework provider does not quote object names
    //     2. in DB2 _ is a reserved character at the beginning of object names
    class DB2HistoryContext : HistoryContext
    {
        public DB2HistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable("MigrationHistory");
        }
    }
}
