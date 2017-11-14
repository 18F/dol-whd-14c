using DOL.WHD.Section14c.Log.Models;
using System;
using System.Linq;

namespace DOL.WHD.Section14c.Log.DataAccess.Repositories
{
    /// <summary>
    /// Error log repository.  Provides access to persisted
    /// logs from some persistence store.
    /// </summary>
    public interface IErrorLogRepository : IDisposable
    {
        /// <summary>
        /// Get all persisted logs
        /// </summary>
        /// <returns>
        /// A list of all logs from persistence
        /// </returns>
        IQueryable<APIErrorLogs> GetAllLogs();

        /// <summary>
        /// Add a new log to the persistent store
        /// </summary>
        /// <param name="entity">
        /// The new log entry to store
        /// </param>
        /// <returns>
        /// The log entry that was stored
        /// </returns>
        LogDetails AddLog(LogDetails entity);
    }
}