using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class ApplicationSubmissionValidator : AbstractValidator<ApplicationSubmission>, IApplicationSubmissionValidator
    {
        public ApplicationSubmissionValidator(IEmployerValidator employerValidator, IHourlyWageInfoValidator hourlyWageInfoValidator, IPieceRateWageInfoValidator pieceRateWageInfoValidator, IWorkSiteValidator workSiteValidator, IWIOAValidator wioaValidator)
        {
            // required
            RuleFor(a => a.RepresentativePayeeSocialSecurityBenefits).NotNull();
            RuleFor(a => a.ProvidingFacilities).NotNull();
            RuleFor(a => a.ReviewedDocumentation).NotNull();
            RuleFor(a => a.EIN).NotEmpty();
            RuleFor(a => a.ApplicationTypeId).NotEmpty();
            RuleFor(a => a.HasPreviousApplication).NotNull();
            RuleFor(a => a.HasPreviousCertificate).NotNull();
            RuleFor(a => a.EstablishmentType).NotNull().Must(et => et.Any());
            RuleFor(a => a.ContactName).NotEmpty();
            RuleFor(a => a.ContactPhone).NotEmpty();
            RuleFor(a => a.ContactEmail).NotEmpty();
            RuleFor(a => a.PayTypeId).NotEmpty();
            RuleFor(a => a.TotalNumWorkSites).NotEmpty();
            RuleFor(a => a.Employer).NotNull().SetValidator(employerValidator);
            RuleFor(a => a.WorkSites).NotNull().Must(w => w.Any()).SetCollectionValidator(workSiteValidator);
            RuleFor(a => a.WIOA).NotNull().SetValidator(wioaValidator);

            // conditional required
            RuleFor(a => a.NumEmployeesRepresentativePayee)
                .NotEmpty()
                .When(a => a.RepresentativePayeeSocialSecurityBenefits.GetValueOrDefault());

            When(a => a.ProvidingFacilities.GetValueOrDefault(), () =>
            {
                RuleFor(a => a.ProvidingFacilitiesDeductionType)
                    .NotNull()
                    .Must(p => p.Any());
                RuleFor(a => a.ProvidingFacilitiesDeductionTypeOther)
                    .NotEmpty()
                    .When(a => a.ProvidingFacilitiesDeductionType.Any(x => x.ProvidingFacilitiesDeductionTypeId == 20));
            });

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
            RuleFor(a => a.WorkSites.Count).Equal(a => a.TotalNumWorkSites.GetValueOrDefault());
        }
    }
}
