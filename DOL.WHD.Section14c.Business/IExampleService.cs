using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Business
{
    public interface IExampleService : IDisposable
    {
        IEnumerable<int> GetNumbers();
        int AddNumbers(IEnumerable<int> numbers);
    }
}
