using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WageTypeInfoValidator<T> : BaseValidator<T> where T : WageTypeInfo
    {
        public WageTypeInfoValidator(IPrevailingWageSurveyInfoValidator prevailingWageSurveyInfoValidator, IAlternateWageDataValidator alternateWageDataValidator)
        {
            RuleFor(w => w.NumWorkers).NotNull();
            RuleFor(w => w.JobName).NotEmpty();
            RuleFor(w => w.JobDescription).NotEmpty();
            RuleFor(w => w.PrevailingWageMethodId).NotNull().InclusiveBetween(ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, ResponseIds.PrevailingWageMethod.SCAWageDetermination);
            RuleFor(w => w.AttachmentId).NotNull();

            // conditional
            RuleFor(w => w.MostRecentPrevailingWageSurvey)
                .NotNull()
                .SetValidator(prevailingWageSurveyInfoValidator)
                .When(w => w.PrevailingWageMethodId == ResponseIds.PrevailingWageMethod.PrevailingWageSurvey);
            RuleFor(w => w.AlternateWageData)
                .NotNull()
                .SetValidator(alternateWageDataValidator)
                .When(w => w.PrevailingWageMethodId == ResponseIds.PrevailingWageMethod.AlternateWageData);
            RuleFor(w => w.SCAWageDeterminationId)
                .NotNull()
                .When(w => w.PrevailingWageMethodId == ResponseIds.PrevailingWageMethod.SCAWageDetermination);
        }
    }
}
