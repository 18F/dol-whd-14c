using System.Collections.Generic;
using System.Web.Http;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Net.Http;
using System.Net;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.Api.Controllers
{
    /// <summary>
    /// Controller for managing application statuses
    /// </summary>
    [RoutePrefix("/api/status")]
    public class StatusController : BaseApiController
    {
        private readonly IStatusService _statusService;

        /// <summary>
        /// Constructor to handle passed status service
        /// </summary>
        /// <param name="statusService"></param>
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// Gets a list of statuses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Status> GetStatuses()
        {
            var statuses = _statusService.GetAllStatuses();
           
            if (statuses == null)
            {
                var message = string.Format("Statuses not found");
                NotFound(message);
            }

            return statuses;
            
        }
    }
}