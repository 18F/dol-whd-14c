using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DOL.WHD.Section14c.Api.Controllers
{
    /// <summary>
    /// Controller for managing application attachment supported file types
    /// </summary>
    [RoutePrefix("/api/SupportedFileTypes")]
    public class SupportedFileTypesController : ApiController
    {
        private readonly IAttachmentSupportedFileTypesService _supportedFileTypesService;

        /// <summary>
        /// Constructor to handle passed supported file types service
        /// </summary>
        /// <param name="supportedFileTypesService"></param>
        public SupportedFileTypesController(IAttachmentSupportedFileTypesService supportedFileTypesService)
        {
            _supportedFileTypesService = supportedFileTypesService;
        }

        /// <summary>
        /// Gets a list of supported file types for attachment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> GetSupportedFileTypes()
        {
            var fileTypes = _supportedFileTypesService.GetAllSupportedFileTypes();

            if (fileTypes == null)
            {
                NotFound();
            }

            return fileTypes;
        }
    }
}
