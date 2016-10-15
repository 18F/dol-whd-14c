using System;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class SaveRepositoryMock : ISaveRepository
    {
        private bool _disposed;
        private readonly Dictionary<string, ApplicationSave> _data;

        public bool Disposed => _disposed;

        public SaveRepositoryMock()
        {
            _data = new Dictionary<string, ApplicationSave>();
            Add(new ApplicationSave
            {
                EIN = "30-1234567",
                ApplicationState = "{ \"name\": \"Barack Obama\", \"email:\" \"president@whitehouse.gov\" }"
            });
        }

        public IQueryable<ApplicationSave> Get()
        {
            return _data.Values.AsQueryable();
        }

        public void Add(ApplicationSave applicationSave)
        {
            _data.Add(applicationSave.EIN, applicationSave);
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
