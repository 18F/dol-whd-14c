using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business
{
    public interface IApplicationService
    {
        Task<int> SubmitApplicationAsync(ApplicationSubmission submission);
        ApplicationSubmission GetApplicationById(Guid id);
        IEnumerable<ApplicationSubmission> GetAllApplications();
        Task<int> ChangeApplicationStatus(ApplicationSubmission application, int newStatusId);
        void ProcessModel(ApplicationSubmission vm);
    }
}
