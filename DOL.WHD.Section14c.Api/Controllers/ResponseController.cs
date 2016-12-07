using System.Collections.Generic;
using System.Web.Http;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Api.Controllers
{
    public class ResponseController : ApiController
    {
        private readonly IResponseService _responseService;

        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        /// <summary>
        /// Returns list of Responses for us on dynamic questions
        /// </summary>
        /// <param name="questionKey">Optional Question Key</param>
        /// <param name="onlyActive">Only return active responses</param>
        /// <returns>All options by default, specific question key limits results.</returns>
        public IEnumerable<Response> Get(string questionKey = null, bool onlyActive = true)
        {
            return _responseService.GetResponses(questionKey, onlyActive);
        }
    }
}
