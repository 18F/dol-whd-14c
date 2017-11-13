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

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sendemail")]
        public IHttpActionResult Concatenate(EmailContent emailContent)
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
