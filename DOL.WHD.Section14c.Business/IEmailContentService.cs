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
    /// <summary>
    /// Email Service Interface
    /// </summary>
    public interface IEmailContentService
    {
        /// <summary>
        /// Prepare Email Contents
        /// </summary>
        /// <param name="application">
        /// Application Data
        /// </param>
        /// <param name="certificationTeamEmailBodyTemplate">
        /// Certification Team Email Body Template
        /// </param>
        /// <param name="employerEmailBodyTemplate">
        /// Certification Team Email Body Template
        /// </param>
        /// <param name="receiver">
        /// Email receivers
        /// </param>
        /// <returns></returns>
        Dictionary<string, EmailContent> PrepareApplicationEmailContents(ApplicationSubmission application, string certificationTeamEmailBodyTemplate, string employerEmailBodyTemplate, EmailReceiver receiver);
    }
}
