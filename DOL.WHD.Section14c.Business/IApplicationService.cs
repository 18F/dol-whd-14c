using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business
{
    public interface IApplicationService
    {
        Task<int> SubmitApplicationAsync(ApplicationSubmission submission);
        ApplicationSubmission CleanupModel(ApplicationSubmission vm);
    }
}
