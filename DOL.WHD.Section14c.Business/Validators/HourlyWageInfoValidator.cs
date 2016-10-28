using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class HourlyWageInfoValidator : WageTypeInfoValidator<HourlyWageInfo>, IHourlyWageInfoValidator
    {
        public HourlyWageInfoValidator(IPrevailingWageSurveyInfoValidator prevailingWageSurveyInfoValidator, IAlternateWageDataValidator alternateWageDataValidator)
            :base(prevailingWageSurveyInfoValidator, alternateWageDataValidator)
        {
            RuleFor(h => h.WorkMeasurementFrequency).NotEmpty();
        }
    }
}
