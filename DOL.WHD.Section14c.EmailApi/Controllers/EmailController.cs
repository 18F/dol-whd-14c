using DOL.WHD.Section14c.EmailApi.Business;
using DOL.WHD.Section14c.EmailApi.Helper;
using DOL.WHD.Section14c.Log.LogHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace DOL.WHD.Section14c.EmailApi.Controllers
{
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
    }
}
