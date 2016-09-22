using System;
using System.Collections.Generic;

namespace DOL.Section14c.DataAccess
{
    public interface IExampleRepository : IDisposable
    {
        IEnumerable<int> GetNumbers();
    }
}
