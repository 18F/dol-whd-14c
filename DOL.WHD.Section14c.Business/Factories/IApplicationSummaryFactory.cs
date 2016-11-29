using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.ViewModels.Summary;

namespace DOL.WHD.Section14c.Business.Factories
{
    public interface IApplicationSummaryFactory
    {
        ApplicationSummary Build(ApplicationSubmission submission);
    }
}
