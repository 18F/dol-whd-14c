using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.DWH.Section14c.DataAccess;

namespace DOL.DWH.Section14c.Business.Test.RepositoryMocks
{
    class ExampleRepositoryMock : IExampleRepository
    {
        public IEnumerable<int> GetNumbers()
        {
            return new List<int>() { 3, 2, 1 };
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
