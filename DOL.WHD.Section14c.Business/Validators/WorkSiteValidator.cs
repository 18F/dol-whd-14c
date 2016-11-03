using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WorkSiteValidator : BaseValidator<WorkSite>, IWorkSiteValidator
    {
        public WorkSiteValidator(IAddressValidator addressValidator, IEmployeeValidator employeeValidator)
        {
            RuleFor(w => w.WorkSiteType).NotNull().Must(wst => wst.Any() && !wst.Any(x => x.WorkSiteTypeId < 27) && !wst.Any(x => x.WorkSiteTypeId > 30));
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Address).NotNull().SetValidator(addressValidator);
            RuleFor(w => w.SCA).NotNull();
            RuleFor(w => w.FederalContractWorkPerformed).NotNull();
            RuleFor(w => w.NumEmployees).NotNull();
            RuleFor(w => w.Employees).NotNull().SetCollectionValidator(employeeValidator);

            RuleFor(w => w.Employees.Count).Equal(w => w.NumEmployees.GetValueOrDefault()).When(w => w.Employees != null);
        }
    }
}
