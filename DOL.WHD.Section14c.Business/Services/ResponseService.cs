using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _repository;
        private bool Disposed = false;
        public ResponseService(IResponseRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Response> GetResponses(string questionKey = null, bool onlyActive = true)
        {
            var responses = _repository.Get();
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

        public Response GetResponseById(string id)
        {
            return _repository.Get().SingleOrDefault(x => x.Id.ToString() == id.ToString());
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _repository.Dispose();
                Disposed = true;
            }
        }
    }
}
