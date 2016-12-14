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
    /// <summary>
    /// Controller to manage application save states
    /// </summary>
    [AuthorizeHttps]
    [RoutePrefix("api/save")]
    public class SaveController : ApiController
    {
        private readonly ISaveService _saveService;
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Constructor to inject services
        /// </summary>
        /// <param name="saveService"></param>
        /// <param name="identityService"></param>
        public SaveController(ISaveService saveService, IIdentityService identityService)
        {
            _saveService = saveService;
            _identityService = identityService;
        }

        /// <summary>
        /// Returns pre-submission 14c application
        /// </summary>
        /// <param name="EIN">Employer Identification Number</param>
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

        /// <summary>
        /// Creates or updates pre-submission 14c application
        /// </summary>
        /// <param name="EIN"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes pre-submission 14c application
        /// </summary>
        /// <param name="EIN"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{EIN}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult DeleteSave(string EIN)
        {
            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                return Unauthorized();
            }

            _saveService.Remove(EIN);
            return Ok();
        }
        
        /// <summary>
        /// OPTIONS endpoint for CORS
        /// </summary>
        [AllowAnonymous]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}