using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class SaveRepositoryMock : ISaveRepository
    {
        private bool _disposed;
        private readonly List<ApplicationSave> _data;

        public bool Disposed => _disposed;

        public SaveRepositoryMock()
        {
            _data = new List<ApplicationSave>
            {
                new ApplicationSave
                {
                    UserId = "1",
                    EIN = "30-1234567",
                    ApplicationState = "{ \"name\": \"Barack Obama\", \"email:\" \"president@whitehouse.gov\" }"
                }
            };
        }

        public IQueryable<ApplicationSave> Get()
        {
            return _data.AsQueryable();
        }

        public void AddOrUpdate(ApplicationSave applicationSave)
        {
            _data.Add(applicationSave);
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
