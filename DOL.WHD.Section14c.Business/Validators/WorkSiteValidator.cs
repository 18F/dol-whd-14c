using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WorkSiteValidator : BaseValidator<WorkSite>, IWorkSiteValidator
    {
        public WorkSiteValidator(IAddressValidatorNoCounty addressValidatorNoCounty, IEmployeeValidator employeeValidator)
        {
            RuleFor(w => w.WorkSiteTypeId).NotNull().GreaterThanOrEqualTo(ResponseIds.WorkSiteType.MainEstablishment).LessThanOrEqualTo(ResponseIds.WorkSiteType.SWEP);
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Address).NotNull().SetValidator(addressValidatorNoCounty);
            RuleFor(w => w.SCA).NotNull();
            RuleFor(w => w.FederalContractWorkPerformed).NotNull();
            RuleFor(w => w.NumEmployees).NotNull();
            RuleFor(w => w.Employees).NotNull().SetCollectionValidator(employeeValidator);

            RuleFor(w => w.Employees.Count).Equal(w => w.NumEmployees.GetValueOrDefault()).When(w => w.Employees != null);
        }
    }
}
