using System.Linq;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class ApplicationSubmissionValidator : BaseValidator<ApplicationSubmission>, IApplicationSubmissionValidator
    {
        public ApplicationSubmissionValidator(ISignatureValidator signatureValidator, IEmployerValidatorInitial employerValidatorInitial, IEmployerValidatorRenewal employerValidatorRenewal, IHourlyWageInfoValidator hourlyWageInfoValidator, IPieceRateWageInfoValidator pieceRateWageInfoValidator, IWorkSiteValidatorInitial workSiteValidatorInitial, IWorkSiteValidatorRenewal workSiteValidatorRenewal, IWIOAValidator wioaValidator)
        {
            // required
            RuleFor(a => a.EIN).NotEmpty();
            RuleFor(a => a.ApplicationTypeId).NotNull().InclusiveBetween(ResponseIds.ApplicationType.Initial, ResponseIds.ApplicationType.Renewal);
            RuleFor(a => a.Signature).NotNull().SetValidator(signatureValidator);
            RuleFor(a => a.ApplicationTypeId).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(2);

            RuleFor(a => a.HasPreviousApplication).NotNull();
            RuleFor(a => a.HasPreviousCertificate).NotNull();
            RuleFor(a => a.EstablishmentType).NotNull().Must(et => et.Any() && !et.Any(x => x.EstablishmentTypeId < ResponseIds.EstablishmentType.WorkCenter) && !et.Any(x => x.EstablishmentTypeId > ResponseIds.EstablishmentType.BusinessEstablishment));
            RuleFor(a => a.ContactFirstName).NotEmpty();
            RuleFor(a => a.ContactLastName).NotEmpty();
            RuleFor(a => a.ContactPhone).NotEmpty();
            RuleFor(a => a.ContactEmail).NotEmpty();
            RuleFor(a => a.PayTypeId).NotEmpty().InclusiveBetween(ResponseIds.PayType.Hourly, ResponseIds.PayType.Both).When(a => a.ApplicationTypeId == ResponseIds.ApplicationType.Renewal);
            RuleFor(a => a.TotalNumWorkSites).NotNull();
            RuleFor(a => a.Employer).NotNull().SetValidator(employerValidatorInitial).When(a => a.ApplicationTypeId == ResponseIds.ApplicationType.Initial);
            RuleFor(a => a.Employer).NotNull().SetValidator(employerValidatorRenewal).When(a => a.ApplicationTypeId == ResponseIds.ApplicationType.Renewal);
            RuleFor(a => a.WorkSites).NotNull().Must(w => w.Any()).SetCollectionValidator(workSiteValidatorInitial).When(a => a.ApplicationTypeId == ResponseIds.ApplicationType.Initial);
            RuleFor(a => a.WorkSites).NotNull().Must(w => w.Any()).SetCollectionValidator(workSiteValidatorRenewal).When(a => a.ApplicationTypeId == ResponseIds.ApplicationType.Renewal);
            RuleFor(a => a.WIOA).NotNull().SetValidator(wioaValidator);

            // conditional required
            RuleFor(a => a.PreviousCertificateNumber)
                .NotEmpty()
                .When(a => a.HasPreviousCertificate.GetValueOrDefault());

            RuleFor(a => a.HourlyWageInfo)
                .NotNull()
                .SetValidator(hourlyWageInfoValidator)
                .When(x => x.PayTypeId == ResponseIds.PayType.Hourly || x.PayTypeId == ResponseIds.PayType.Both);

            RuleFor(a => a.PieceRateWageInfo)
                .NotNull()
                .SetValidator(pieceRateWageInfoValidator)
                .When(x => x.PayTypeId == ResponseIds.PayType.PieceRate || x.PayTypeId == ResponseIds.PayType.Both);

            // Other validation
            RuleFor(a => a.ContactEmail).EmailAddress();
            RuleFor(a => a.WorkSites.Count)
                .Equal(a => a.TotalNumWorkSites.GetValueOrDefault())
                .When(a => a.WorkSites != null);
        }
    }
}
