using DOL.WHD.Section14c.EmailApi.Business;
using DOL.WHD.Section14c.EmailApi.Helper;
using DOL.WHD.Section14c.Log.LogHelper;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DOL.WHD.Section14c.EmailApi.Controllers
{
    /// <summary>
    /// Email API Controllers
    /// </summary>
    [RoutePrefix("api/email")]
    public class EmailController : BaseApiController
    {
        private IEmailService _emailService;
        /// <summary>
        /// Email Controller
        /// </summary>
        /// <param name="emailService"></param>
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="emailContent"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sendemail")]
        public IHttpActionResult SendEmail(EmailContent emailContent)
        {
            if (emailContent == null)
            {
                throw new ArgumentNullException(nameof(emailContent));
            }
            
            var result = _emailService.SendEmail(emailContent);

            return Ok(result);
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
