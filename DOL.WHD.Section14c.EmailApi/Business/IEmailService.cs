using DOL.WHD.Section14c.EmailApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.EmailApi.Business
{
    public interface IEmailService
    {
        bool SendEmail(EmailContent emailContent);
    }
}
