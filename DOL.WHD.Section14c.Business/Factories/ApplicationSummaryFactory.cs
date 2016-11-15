using System.Linq;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business.Factories
{
    public class ApplicationSummaryFactory : IApplicationSummaryFactory
    {
        public ApplicationSummary Build(ApplicationSubmission submission)
        {
            var obj = new ApplicationSummary
            {
                Id = submission.Id,
                ApplicationType = submission.ApplicationType.Display,
                CertificateEffectiveDate = submission.CertificateEffectiveDate,
                CertificateExpirationDate = submission.CertificateExpirationDate,
                CertificateNumber = submission.CertificateNumber,
                CertificateType = submission.EstablishmentType.Select(x => x.EstablishmentType.Display),
                NumWorkers = submission.WorkSites.Sum(x => x.Employees.Count),
                NumWorkSites = submission.WorkSites.Count,
                State = submission.Employer.PhysicalAddress.State,
                StatusName = submission.Status.Name
            };

            return obj;
        }
    }
}
