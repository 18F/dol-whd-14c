using System.Collections.Generic;
using System.Web.Http;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Api.Controllers
{
    public class ResponseController : ApiController
    {
        private readonly IResponseRepository _responseRepository;

        public ResponseController(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }

        public IEnumerable<Response> Get(string questionKey = null, bool onlyActive = true)
        {
            return _responseRepository.GetResponses(questionKey, onlyActive);
        }
    }
}
