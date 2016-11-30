using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class EmployerValidatorRenewal : EmployerValidatorInitial, IEmployerValidatorRenewal
    {
        public EmployerValidatorRenewal(IAddressValidator addressValidator, IWorkerCountInfoValidator workerCountInfoValidator)
            :base(addressValidator)
        {
            RuleFor(e => e.FiscalQuarterEndDate).NotEmpty();
            RuleFor(e => e.NumSubminimalWageWorkers).NotNull().SetValidator(workerCountInfoValidator);
        }
    }
}
