using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class EmployerValidator : BaseValidator<EmployerInfo>, IEmployerValidator
    {
        public EmployerValidator(IAddressValidator addressValidator, IWorkerCountInfoValidator workerCountInfoValidator)
        {
            // required
            RuleFor(e => e.LegalName).NotEmpty();
            RuleFor(e => e.HasTradeName).NotNull();
            RuleFor(e => e.LegalNameHasChanged).NotNull();
            RuleFor(e => e.PhysicalAddress).NotNull().SetValidator(addressValidator);
            RuleFor(e => e.HasParentOrg).NotNull();
            RuleFor(e => e.EmployerStatusId).NotNull().GreaterThanOrEqualTo(7).LessThanOrEqualTo(10);
            RuleFor(e => e.IsEducationalAgency).NotNull();
            RuleFor(e => e.FiscalQuarterEndDate).NotEmpty();
            RuleFor(e => e.NumSubminimalWageWorkers).NotNull().SetValidator(workerCountInfoValidator);
            RuleFor(e => e.PCA).NotNull();
            RuleFor(e => e.SCAId).NotNull().GreaterThanOrEqualTo(11).LessThanOrEqualTo(13);
            RuleFor(e => e.EO13658Id).NotNull().GreaterThanOrEqualTo(14).LessThanOrEqualTo(16);
            RuleFor(e => e.RepresentativePayee).NotNull();
            RuleFor(e => e.TakeCreditForCosts).NotNull();
            RuleFor(e => e.TemporaryAuthority).NotNull();

            // conditional required
            When(a => a.TakeCreditForCosts.GetValueOrDefault(), () =>
            {
                RuleFor(a => a.ProvidingFacilitiesDeductionType)
                    .NotNull()
                    .Must(p => p.Any() && !p.Any(x => x.ProvidingFacilitiesDeductionTypeId < 17) && !p.Any(x => x.ProvidingFacilitiesDeductionTypeId > 20));
                RuleFor(a => a.ProvidingFacilitiesDeductionTypeOther)
                    .NotEmpty()
                    .When(a => a.ProvidingFacilitiesDeductionType != null && a.ProvidingFacilitiesDeductionType.Any(x => x.ProvidingFacilitiesDeductionTypeId == 20));
            });
            RuleFor(e => e.TradeName).NotEmpty().When(e => e.HasTradeName.GetValueOrDefault());
            RuleFor(e => e.PriorLegalName).NotEmpty().When(e => e.LegalNameHasChanged.GetValueOrDefault());
            When(e => e.HasParentOrg.GetValueOrDefault(), () =>
            {
                RuleFor(e => e.ParentLegalName).NotEmpty();
                RuleFor(e => e.ParentTradeName).NotEmpty();
                RuleFor(e => e.ParentAddress).NotNull().SetValidator(addressValidator);
                RuleFor(e => e.SendMailToParent).NotNull();
            });
            RuleFor(e => e.EmployerStatusOther).NotEmpty().When(e => e.EmployerStatusId == 10);
            When(e => e.SCAId == 11, () =>
            {
                RuleFor(e => e.SCACount).NotNull();
                RuleFor(e => e.SCAAttachmentId).NotNull();
            });
        }
    }
}
