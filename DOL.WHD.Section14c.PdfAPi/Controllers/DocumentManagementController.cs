using DOL.WHD.Section14c.Log.LogHelper;
using DOL.WHD.Section14c.PdfApi.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DOL.WHD.Section14c.PdfApi.Controllers
{
    [RoutePrefix("api/DocumentManagement")]
    public class DocumentManagementController : BaseApiController
    {
        private IDocumentConcatenate _documentConcatenateService;

        public DocumentManagementController(IDocumentConcatenate documentConcatenateService)
        {
            _documentConcatenateService = documentConcatenateService;
        }

        [HttpGet]
        [Route("Concatenate")]
        public IHttpActionResult Concatenate(List<byte[]> documentContentByteArrays)
        {
            var result = _documentConcatenateService.Concatenate(documentContentByteArrays);
            if (result == null)
            {
                InternalServerError("An error occurred, please try again.");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Concatenate")]
        public IHttpActionResult Concatenate(List<string> filePaths)
        {
            var result = _documentConcatenateService.Concatenate(filePaths);
            if (result == null)
            {
                InternalServerError("An error occurred, please try again.");
            }
            return Ok(result);
        }
    }
}
