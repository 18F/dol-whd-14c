using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.ViewModels.Summary;

namespace DOL.WHD.Section14c.Business.Factories
{
    public class ApplicationSummaryFactory : IApplicationSummaryFactory
    {
        public ApplicationSummary Build(ApplicationSubmission submission)
        {
            var obj = new ApplicationSummary
            {
                Id = submission.Id,
                ApplicationType = BuildResponseSummary(submission.ApplicationType),
                CertificateEffectiveDate = submission.CertificateEffectiveDate,
                CertificateExpirationDate = submission.CertificateExpirationDate,
                CertificateNumber = submission.CertificateNumber,
                CertificateType = submission.EstablishmentType.Select(x => BuildResponseSummary(x.EstablishmentType)),
                NumWorkers = submission.WorkSites.Sum(x => x.Employees.Count),
                NumWorkSites = submission.WorkSites.Count,
                State = submission.Employer.PhysicalAddress.State,
                Status = BuildStatusSummary(submission.Status),
                EmployerName = submission.Employer.LegalName
            };

            return obj;
        }

        private ResponseSummary BuildResponseSummary(Response response)
        {
            var obj = new ResponseSummary
            {
                Id = response.Id,
                Display = response.Display,
                ShortDisplay = response.ShortDisplay
            };

            return obj;
        }

        private StatusSummary BuildStatusSummary(Status status)
        {
            var obj = new StatusSummary
            {
                Id = status.Id,
                Name = status.Name
            };

            return obj;
        }
    }
}
