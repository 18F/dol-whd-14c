using DOL.WHD.Section14c.Log.LogHelper;
using DOL.WHD.Section14c.PdfApi.Business;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace DOL.WHD.Section14c.PdfApi.Controllers
{
    [RoutePrefix("api/documentmanagement")]
    public class DocumentManagementController : BaseApiController
    {
        private IDocumentConcatenate _documentConcatenateService;

        public DocumentManagementController(IDocumentConcatenate documentConcatenateService)
        {
            _documentConcatenateService = documentConcatenateService;
        }

        /// <summary>
        /// Concatenate PDF from byte arrays
        /// </summary>
        /// <param name="documentContentByteArrays"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("concatenate")]
        public IHttpActionResult Concatenate(List<PDFContentData> applicationDataCollection)
        {
            if (applicationDataCollection == null)
            {
                throw new ArgumentNullException(nameof(applicationDataCollection));
            }

            var buffer = _documentConcatenateService.Concatenate(applicationDataCollection);

            return Ok(buffer);
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
