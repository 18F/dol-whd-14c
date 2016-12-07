using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class SourceEmployerValidator : BaseValidator<SourceEmployer>, ISourceEmployerValidator
    {
        public SourceEmployerValidator(IAddressValidatorNoCounty addressValidatorNoCounty)
        {
            RuleFor(s => s.EmployerName).NotEmpty();
            RuleFor(s => s.Address).NotNull().SetValidator(addressValidatorNoCounty);
            RuleFor(s => s.Phone).NotEmpty();
            RuleFor(s => s.ContactName).NotEmpty();
            RuleFor(s => s.ContactTitle).NotEmpty();
            RuleFor(s => s.ContactDate).NotEmpty();
            RuleFor(s => s.JobDescription).NotEmpty();
            RuleFor(s => s.ExperiencedWorkerWageProvided).NotEmpty();
            RuleFor(s => s.ConclusionWageRateNotBasedOnEntry).NotEmpty();
        }
    }
}
