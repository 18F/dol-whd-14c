using System.Collections.Concurrent;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class SaveRepositoryMock : ISaveRepository
    {
        private bool _disposed;
        private readonly ConcurrentDictionary<string, ApplicationSave> _data;

        public bool Disposed => _disposed;

        public SaveRepositoryMock()
        {
            _data = new ConcurrentDictionary<string, ApplicationSave>();
            AddOrUpdate(new ApplicationSave
            {
                ApplicationId = "CE7F5AA5-6832-47FE-BAE1-80D14CD8F667",
            ApplicationState = "{ \"name\": \"Barack Obama\", \"email:\" \"president@whitehouse.gov\" }"
            });
        }

        public IQueryable<ApplicationSave> Get()
        {
            return _data.Values.AsQueryable();
        }

        public void AddOrUpdate(ApplicationSave applicationSave)
        {
            _data.AddOrUpdate(applicationSave.ApplicationId, applicationSave, (key, oldValue) => applicationSave);
        }

        public void Remove(string applicationId)
        {
            ApplicationSave save;
            _data.TryRemove(applicationId, out save);
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _disposed = _disposed || disposing;
        }
    }
}
