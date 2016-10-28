using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>, IEmployeeValidator
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.PrimaryDisabilityId).NotEmpty();
            RuleFor(e => e.WorkType).NotEmpty();
            RuleFor(e => e.NumJobs).NotEmpty();
            RuleFor(e => e.AvgWeeklyHours).NotEmpty();
            RuleFor(e => e.AvgHourlyEarnings).NotEmpty();
            RuleFor(e => e.PrevailingWage).NotEmpty();
            RuleFor(e => e.ProductivityMeasure).NotEmpty();
            RuleFor(e => e.CommensurateWageRate).NotEmpty();
            RuleFor(e => e.TotalHours).NotEmpty();
            RuleFor(e => e.WorkAtOtherSite).NotNull();

            // conditional
            RuleFor(e => e.PrimaryDisabilityOther).NotEmpty().When(e => e.PrimaryDisabilityId == 38);
        }
    }
}
