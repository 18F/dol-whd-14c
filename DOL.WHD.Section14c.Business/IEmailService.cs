using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.EmailApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Business.Helper;

namespace DOL.WHD.Section14c.Business
{
    public interface IEmailService
    {
        Dictionary<string, EmailContent> PrepareApplicationEmailContents(ApplicationSubmission application, string emailContents, EmailReceiver receiver);
    }
}
