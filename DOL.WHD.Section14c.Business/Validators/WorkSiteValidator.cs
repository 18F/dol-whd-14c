using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WorkSiteValidator : AbstractValidator<WorkSite>, IWorkSiteValidator
    {
        public WorkSiteValidator(IAddressValidator addressValidator, IEmployeeValidator employeeValidator)
        {
            RuleFor(w => w.WorkSiteType).NotNull().Must(wst => wst.Any());
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Address).NotNull().SetValidator(addressValidator);
            RuleFor(w => w.SCA).NotNull();
            RuleFor(w => w.FederalContractWorkPerformed).NotNull();
            RuleFor(w => w.NumEmployees).NotEmpty();
            RuleFor(w => w.Employees).NotNull().SetCollectionValidator(employeeValidator);

            RuleFor(w => w.Employees.Count).Equal(w => w.NumEmployees).When(w => w.Employees != null);
        }
    }
}
