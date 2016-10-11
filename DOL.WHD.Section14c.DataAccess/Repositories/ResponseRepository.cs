using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ResponseRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Response> GetResponses(string questionKey = null, bool onlyActive = true)
        {
            var responses = _dbContext.Responses.Select(x => x);
            if (!string.IsNullOrEmpty(questionKey))
            {
                responses = responses.Where(x => x.QuestionKey == questionKey);
            }
            if (onlyActive)
            {
                responses = responses.Where(x => x.IsActive);
            }

            return responses.ToList();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
