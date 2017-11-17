using DOL.WHD.Section14c.EmailApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.EmailApi.Business
{
    /// <summary>
    /// Email Service Interface
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send Email via SMTP
        /// </summary>
        /// <param name="emailContent">
        /// Email Content object. it includes the following: 
        /// Email To,Email CC,Email subject,Email body,Email attachment
        /// </param>
        /// <returns>boolean success or fail</returns>
        bool SendEmail(EmailContent emailContent);
    }
}
