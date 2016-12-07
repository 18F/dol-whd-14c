using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business.Factories
{
    public interface IApplicationSummaryFactory
    {
        ApplicationSummary Build(ApplicationSubmission submission);
    }
}
