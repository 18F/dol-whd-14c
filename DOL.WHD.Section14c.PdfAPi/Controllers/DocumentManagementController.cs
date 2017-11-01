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
    [RoutePrefix("api/documentmanagement")]
    public class DocumentManagementController : BaseApiController
    {
        private IDocumentConcatenate _documentConcatenateService;
        private IPdfDownloadService _pdfDownloadService;

        public DocumentManagementController(IDocumentConcatenate documentConcatenateService, IPdfDownloadService pdfDownloadService)
        {
            _documentConcatenateService = documentConcatenateService;
            _pdfDownloadService = pdfDownloadService;
        }

        /// <summary>
        /// Concatenate PDF from byte arrays
        /// </summary>
        /// <param name="documentContentByteArrays"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("concatenate")]
        [AllowAnonymous]
        public IHttpActionResult Concatenate(List<ApplicationData> applicationDataCollection)
        {
            var pdfDocument = _documentConcatenateService.Concatenate(applicationDataCollection);            
            var response = _pdfDownloadService.Download(pdfDocument, Request);
         
            return Ok(response);
        }
    }
}
