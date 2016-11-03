using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class ApplicationSubmissionValidator : BaseValidator<ApplicationSubmission>, IApplicationSubmissionValidator
    {
        public ApplicationSubmissionValidator(IEmployerValidator employerValidator, IHourlyWageInfoValidator hourlyWageInfoValidator, IPieceRateWageInfoValidator pieceRateWageInfoValidator, IWorkSiteValidator workSiteValidator, IWIOAValidator wioaValidator)
        {
            // required
            RuleFor(a => a.EIN).NotEmpty();
            RuleFor(a => a.ApplicationTypeId).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(2);
            RuleFor(a => a.HasPreviousApplication).NotNull();
            RuleFor(a => a.HasPreviousCertificate).NotNull();
            RuleFor(a => a.EstablishmentType).NotNull().Must(et => et.Any() && !et.Any(x => x.EstablishmentTypeId < 3) && !et.Any(x => x.EstablishmentTypeId > 6));
            RuleFor(a => a.ContactName).NotEmpty();
            RuleFor(a => a.ContactPhone).NotEmpty();
            RuleFor(a => a.ContactEmail).NotEmpty();
            RuleFor(a => a.PayTypeId).NotEmpty().GreaterThanOrEqualTo(21).LessThanOrEqualTo(23);
            RuleFor(a => a.TotalNumWorkSites).NotNull();
            RuleFor(a => a.Employer).NotNull().SetValidator(employerValidator);
            RuleFor(a => a.WorkSites).NotNull().Must(w => w.Any()).SetCollectionValidator(workSiteValidator);
            RuleFor(a => a.WIOA).NotNull().SetValidator(wioaValidator);

            // conditional required
            RuleFor(a => a.CertificateNumber)
                .NotEmpty()
                .When(a => a.HasPreviousCertificate.GetValueOrDefault());

            RuleFor(a => a.HourlyWageInfo)
                .NotNull()
                .SetValidator(hourlyWageInfoValidator)
                .When(x => x.PayTypeId == 21 || x.PayTypeId == 23);

            RuleFor(a => a.PieceRateWageInfo)
                .NotNull()
                .SetValidator(pieceRateWageInfoValidator)
                .When(x => x.PayTypeId == 22 || x.PayTypeId == 23);

            // Other validation
            RuleFor(a => a.ContactEmail).EmailAddress();
            RuleFor(a => a.WorkSites.Count)
                .Equal(a => a.TotalNumWorkSites.GetValueOrDefault())
                .When(a => a.WorkSites != null);
        }
    }
}
