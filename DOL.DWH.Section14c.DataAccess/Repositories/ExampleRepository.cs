using System.Collections.Generic;
using System.Linq;

namespace DOL.DWH.Section14c.DataAccess.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExampleRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IEnumerable<int> GetNumbers()
        {
            return _dbContext.Numbers.Select(x => x.Number);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
