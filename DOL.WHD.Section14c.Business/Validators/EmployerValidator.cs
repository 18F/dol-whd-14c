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
            RuleFor(e => e.HasDifferentMailingAddress).NotNull();
            RuleFor(e => e.HasParentOrg).NotNull();
            RuleFor(e => e.EmployerStatusId).NotNull();
            RuleFor(e => e.IsEducationalAgency).NotNull();
            RuleFor(e => e.FiscalQuarterEndDate).NotEmpty();
            RuleFor(e => e.NumSubminimalWageWorkers).NotNull().SetValidator(workerCountInfoValidator);
            RuleFor(e => e.PCA).NotNull();
            RuleFor(e => e.SCAId).NotNull();
            RuleFor(e => e.EO13658Id).NotNull();
            RuleFor(e => e.RepresentativePayee).NotNull();
            RuleFor(e => e.TakeCreditForCosts).NotNull();
            RuleFor(e => e.ProvidingFacilitiesDeductionTypeId).NotNull();
            RuleFor(e => e.TemporaryAuthority).NotNull();

            // conditional required
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
            RuleFor(e => e.ProvidingFacilitiesDeductionTypeOther)
                .NotEmpty()
                .When(e => e.ProvidingFacilitiesDeductionTypeId == 20);
        }
    }
}
