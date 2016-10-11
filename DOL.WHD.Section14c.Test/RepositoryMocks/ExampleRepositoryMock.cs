using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.DataAccess;

namespace DOL.WHD.Section14c.Business.Test.RepositoryMocks
{
    class ExampleRepositoryMock : IExampleRepository
    {
        private bool _disposed;
        public bool Disposed => _disposed;

        public IEnumerable<int> GetNumbers()
        {
            return new List<int>() { 3, 2, 1 };
        }
        public void Dispose()
        {
            _disposed = true;
        }
    }
}
