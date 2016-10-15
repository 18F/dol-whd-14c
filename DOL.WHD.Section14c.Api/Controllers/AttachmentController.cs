using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using DOL.WHD.Section14c.Api.Providers;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/attachment")]
    public class AttachmentController : ApiController
    {
        private readonly ISaveService _saveService;
        private readonly IIdentityService _identityService;

        public AttachmentController(ISaveService saveService, IIdentityService identityService)
        {
            _saveService = saveService;
            _identityService = identityService;
        }
        [Route("{EIN}")]
        public async Task<IHttpActionResult> Post(string EIN)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }

            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                return Unauthorized();
            }

            var filesReadToProvider = await Request.Content.ReadAsMultipartAsync(new RestrictedMultipartMemoryStreamProvider());
            var files = new List<Domain.Models.Submission.Attachment>();
            foreach (var stream in filesReadToProvider.Contents)
            {
                if (stream.Headers.ContentLength == 0)
                    return BadRequest("Invalid file.");

                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    var fileName = stream.Headers.ContentDisposition.FileName.Replace("\"", "");
                    var fileType = stream.Headers.ContentType.MediaType.Replace("\"", "");
                    var fileUpload = _saveService.UploadAttachment(EIN, memoryStream, fileName, fileType);
                    files.Add(fileUpload);
                }

            }
            return Ok(files);
        }

        [HttpGet]
        [Route("{EIN}/{fileId}")]
        public HttpResponseMessage Download(string EIN, Guid fileId)
        {
            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            try
            {
                var memoryStream = new MemoryStream();  // Disponsed by Framework
                
                var attachmentDownload = _saveService.DownloadAttachment(memoryStream, EIN, fileId);

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(attachmentDownload.MemoryStream) // Disponsed by Framework
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(attachmentDownload.Attachment.MimeType);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = attachmentDownload.Attachment.OriginalFileName
                };
                return result;
                
            }
            catch (Exception ex)
            {
                if (ex is ObjectNotFoundException || ex is FileNotFoundException)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                throw;
            }
        }

        [HttpDelete]
        [Route("{EIN}/{fileId}")]
        public IHttpActionResult Delete(string EIN, Guid fileId)
        {
            // make sure user has rights to the EIN
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                return Unauthorized();
            }

            try
            {
                _saveService.DeleteAttachement(EIN, fileId);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}