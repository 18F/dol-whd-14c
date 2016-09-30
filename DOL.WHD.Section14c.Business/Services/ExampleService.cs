using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;

namespace DOL.WHD.Section14c.Business.Services
{
    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository _exampleRepository;

        public ExampleService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public IEnumerable<int> GetNumbers()
        {
            return _exampleRepository.GetNumbers().OrderBy(x => x);
        }

        public int AddNumbers(IEnumerable<int> numbers)
        {
            return numbers.Sum();
        }

        public void Dispose()
        {
            _exampleRepository.Dispose();
        }
    }
}
