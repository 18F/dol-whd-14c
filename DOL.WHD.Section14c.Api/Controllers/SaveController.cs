using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace DOL.WHD.Section14c.Api.Controllers
{
    [AuthorizeHttps]
    [RoutePrefix("api/save")]
    public class SaveController : ApiController
    {
        private readonly ISaveService _saveService;
        private readonly IIdentityService _identityService;

        public SaveController(ISaveService saveService, IIdentityService identityService)
        {
            _saveService = saveService;
            _identityService = identityService;
        }

        [HttpGet]
        [Route("{EIN}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult GetSave(string EIN)
        {
            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                return Unauthorized();
            }

            var applicationSave = _saveService.GetSave(EIN);
            if (applicationSave != null)
            {
                return Ok(applicationSave.ApplicationState);
            }
            
            return NotFound();
        }

        [HttpPost]
        [Route("{EIN}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult AddSave(string EIN)
        {
            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                return Unauthorized();
            }

            var state = Request.Content.ReadAsStringAsync().Result;
            try
            {
                JToken.Parse(state);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            _saveService.AddOrUpdate(EIN, state);
            return Created($"/api/Save?userId={User.Identity.GetUserId()}&EIN={EIN}", new { });
        }

        [AllowAnonymous]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}