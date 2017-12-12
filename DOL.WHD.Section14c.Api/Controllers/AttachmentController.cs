using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.Api.Providers;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Common;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.Api.Controllers
{
    /// <summary>
    /// Attachment API controller, for handling file uploads and downloads.
    /// These are attachments to 14(c) applications.
    /// </summary>
    [AuthorizeHttps]
    [RoutePrefix("api/attachment")]
    public class AttachmentController : BaseApiController
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attachmentService">
        /// The attachment service this controller should use
        /// </param>
        /// <param name="identityService">
        /// The identity service this controller should use
        /// </param>
        public AttachmentController(IAttachmentService attachmentService, IIdentityService identityService)
        {
            _attachmentService = attachmentService;
            _identityService = identityService;
        }

        /// <summary>
        /// Upload Attachment        
        /// </summary>
        /// <param name="EIN">Employer Identification Number</param>
        /// <returns>Http status code</returns>
        [Route("{Id}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public async Task<IHttpActionResult> Post(string EIN)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }

            // make sure user has rights to the Id
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                Unauthorized("Unauthorized");
            }

            var filesReadToProvider = await Request.Content.ReadAsMultipartAsync(new RestrictedMultipartMemoryStreamProvider());

            var files = new List<Domain.Models.Submission.Attachment>();
            var allowedMaximumContentLength = AppSettings.Get<int>("AllowedMaximumContentLength");
            foreach (var stream in filesReadToProvider.Contents)
            {
                // The code that handle the max allowed length at the IIS level within web.config.
                // as well as within the httpRuntime attribute found in the system.web section
                var bytes = await stream.ReadAsByteArrayAsync();

                if (bytes.Length < 1 || bytes.Length > allowedMaximumContentLength)
                {
                    BadRequest("Invalid file size.");
                }
                var fileName = stream.Headers.ContentDisposition.FileName.Replace("\"", "");
                var fileType = stream.Headers.ContentType.MediaType.Replace("\"", "");
                var fileUpload = _attachmentService.UploadAttachment(EIN, bytes, fileName, fileType);
                files.Add(fileUpload);
            }
            return Ok(files);
        }

        /// <summary>
        /// Download attachment by Id
        /// </summary>
        /// <param name="EIN">Employer Identification Number</param>
        /// <param name="fileId">File Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id}/{fileId}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication, ApplicationClaimTypes.ViewAllApplications)]
        public IHttpActionResult Download(string EIN, Guid fileId)
        {
            // make sure user has rights to the Id or has View All Application rights
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            var hasViewAllFeature = _identityService.UserHasFeatureClaim(User, ApplicationClaimTypes.ViewAllApplications);
            if (!hasEINClaim && !hasViewAllFeature)
            {
                Unauthorized("User doesn't have rights to download attachments from this Id");
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var memoryStream = new MemoryStream();  // Disponsed by Framework

                var attachmentDownload = _attachmentService.DownloadAttachment(memoryStream, EIN, fileId);

                result.Content = new StreamContent(attachmentDownload.MemoryStream); // Disponsed by Framework

                result.Content.Headers.ContentType = new MediaTypeHeaderValue(attachmentDownload.Attachment.MimeType);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = attachmentDownload.Attachment.OriginalFileName
                };

            }
            catch (Exception ex)
            {
                if (ex is ObjectNotFoundException || ex is FileNotFoundException)
                {
                    NotFound("Not found");
                }

                throw;
            }
            return ResponseMessage(result); //result;
        }

        /// <summary>
        /// Delete Attachment by Id
        /// </summary>
        /// <param name="EIN">Employer Identification Number</param>
        /// <param name="fileId">File Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{Id}/{fileId}")]
        [AuthorizeClaims(ApplicationClaimTypes.SubmitApplication)]
        public IHttpActionResult Delete(string EIN, Guid fileId)
        {
            // make sure user has rights to the Id
            var hasEINClaim = _identityService.UserHasEINClaim(User, EIN);
            if (!hasEINClaim)
            {
                Unauthorized("Unauthorized");
            }

            try
            {
                _attachmentService.DeleteAttachement(EIN, fileId);
            }
            catch (ObjectNotFoundException)
            {
                NotFound("Not found");
            }

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