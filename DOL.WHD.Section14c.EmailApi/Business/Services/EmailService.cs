using DOL.WHD.Section14c.EmailApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using DOL.WHD.Section14c.Log.LogHelper;
using System.Net;

namespace DOL.WHD.Section14c.EmailApi.Business
{
    public class EmailService: IEmailService
    {
        private SmtpClient _smtpClient;

        ///// <summary>
        ///// Set SmtpClient
        ///// </summary>
        //public EmailService(SmtpClient smtpClient)
        //{
        //    _smtpClient = smtpClient ?? new SmtpClient();
        //}

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="emailContent"></param>
        /// <returns></returns>
        public bool SendEmail(EmailContent emailContent)
        {
            bool success = true;

            try
            {
                if (emailContent == null)
                    throw new ArgumentNullException("Email Content Exception", "Send Email");
                _smtpClient = new SmtpClient();

                MailMessage mailMessage = new MailMessage();

                // Allow multiple Recipients with MailMessage
                foreach (var address in emailContent.To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.To.Add(address);
                }

                // Add Emaill CC
                if (!string.IsNullOrEmpty(emailContent.CC))
                {
                    foreach (var address in emailContent.CC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mailMessage.CC.Add(address);
                    }
                }
                
                // Add Attachments
                foreach (var attachment in emailContent.attachments)
                {
                    mailMessage.Attachments.Add(attachment);
                }

                mailMessage.Subject = emailContent.Subject;
                mailMessage.Body = emailContent.Body;
                
                _smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                success = false;
                if (ex is InvalidOperationException)
                {
                    throw new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, HttpStatusCode.InternalServerError);
                }
                throw;
            }

            return success;
        }        
    }
}