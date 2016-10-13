using System.Web.Http;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.ViewModels;
using Newtonsoft.Json.Linq;

namespace DOL.WHD.Section14c.Api.Controllers
{
    public class SaveController : ApiController
    {
        private readonly ISaveService _saveService;

        public SaveController(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public JObject GetSave(string userId, string EIN)
        {
            var json = _saveService.GetSave(userId, EIN);
            JObject jsonObj = JObject.Parse(json);
            return jsonObj;
        }

        public IHttpActionResult AddSave([FromBody]AddApplicationSave vm)
        {
            _saveService.AddOrUpdate(vm.UserId, vm.EIN, vm.State.ToString());
            return Created($"/api/Save?userId={vm.UserId}&EIN={vm.EIN}", new { });
        }
    }
}