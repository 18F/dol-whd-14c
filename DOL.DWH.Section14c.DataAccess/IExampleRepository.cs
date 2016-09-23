using System;
using System.Collections.Generic;

namespace DOL.DWH.Section14c.DataAccess
{
    public interface IExampleRepository : IDisposable
    {
        IEnumerable<int> GetNumbers();
    }
}
