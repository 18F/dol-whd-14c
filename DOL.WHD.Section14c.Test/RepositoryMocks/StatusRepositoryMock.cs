using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    /// <summary>
    /// Status repository mock
    /// </summary>
    public class StatusRepositoryMock : IStatusRepository
    {
        private bool _disposed;
        private readonly List<Status> _data;

        public bool Disposed => _disposed;

        /// <summary>
        /// Sets up the mock with fake data
        /// </summary>
        public StatusRepositoryMock()
        {
            _data = new List<Status>
            {
                new Status {Id = StatusIds.Pending, Name = "Pending", IsActive = true},
                new Status {Id = StatusIds.Issued, Name = "Issued", IsActive = true},
                new Status {Id = StatusIds.Withdrawn, Name = "Withdrawn", IsActive = true},
                new Status {Id = StatusIds.Amending, Name = "Amending", IsActive = true},
                new Status {Id = StatusIds.Denied, Name = "Denied", IsActive = true},
                new Status {Id = StatusIds.Revoked, Name = "Revoked", IsActive = true},
                new Status {Id = StatusIds.Expired, Name = "Expired", IsActive = true}
            };
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <returns>Data</returns>
        public IQueryable<Status> Get()
        {
            return _data.AsQueryable();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }
    }
}
