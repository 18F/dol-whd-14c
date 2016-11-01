using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class EmployeeValidator : BaseValidator<Employee>, IEmployeeValidator
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.PrimaryDisabilityId).NotNull();
            RuleFor(e => e.WorkType).NotEmpty();
            RuleFor(e => e.NumJobs).NotNull();
            RuleFor(e => e.AvgWeeklyHours).NotNull();
            RuleFor(e => e.AvgHourlyEarnings).NotNull();
            RuleFor(e => e.PrevailingWage).NotNull();
            RuleFor(e => e.ProductivityMeasure).NotNull();
            RuleFor(e => e.CommensurateWageRate).NotEmpty();
            RuleFor(e => e.TotalHours).NotNull();
            RuleFor(e => e.WorkAtOtherSite).NotNull();

            // conditional
            RuleFor(e => e.PrimaryDisabilityOther).NotEmpty().When(e => e.PrimaryDisabilityId == 38);
        }
    }
}
