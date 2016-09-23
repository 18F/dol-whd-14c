using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using DOL.Section14c.Business;

namespace DOL.Section14c.Api.Controllers
{
    [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]
    public class ExampleController : ApiController
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        // GET api/<controller>
        public IEnumerable<int> Get()
        {
            return _exampleService.GetNumbers();
        }

        public int AddNumbers(IEnumerable<int> numbers)
        {
            return _exampleService.AddNumbers(numbers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _exampleService?.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}