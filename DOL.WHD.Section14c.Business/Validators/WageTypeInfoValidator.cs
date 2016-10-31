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
            RuleFor(w => w.PrevailingWageMethodId).NotNull();
            RuleFor(w => w.AttachmentId).NotNull();

            // conditional
            RuleFor(w => w.MostRecentPrevailingWageSurvey)
                .NotNull()
                .SetValidator(prevailingWageSurveyInfoValidator)
                .When(w => w.PrevailingWageMethodId == 24);
            RuleFor(w => w.AlternateWageData)
                .NotNull()
                .SetValidator(alternateWageDataValidator)
                .When(w => w.PrevailingWageMethodId == 25);
            RuleFor(w => w.SCAWageDeterminationId)
                .NotNull()
                .When(w => w.PrevailingWageMethodId == 26);
        }
    }
}
