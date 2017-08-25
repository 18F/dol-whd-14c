using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WorkSiteValidatorRenewal : WorkSiteValidatorInitial, IWorkSiteValidatorRenewal
    {
        public WorkSiteValidatorRenewal(IAddressValidatorNoCounty addressValidatorNoCounty, IEmployeeValidator employeeValidator)
            :base(addressValidatorNoCounty)
        {
            RuleFor(w => w.NumEmployees).NotNull();
            RuleFor(w => w.Employees).NotNull().SetCollectionValidator(employeeValidator);

            RuleFor(w => w.Employees.Count).Equal(w => w.NumEmployees.GetValueOrDefault()).When(w => w.Employees != null);
        }
    }
}
