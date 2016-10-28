using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class PrevailingWageSurveyInfoValidator : AbstractValidator<PrevailingWageSurveyInfo>, IPrevailingWageSurveyInfoValidator
    {
        public PrevailingWageSurveyInfoValidator(ISourceEmployerValidator sourceEmployerValidator)
        {
            RuleFor(p => p.PrevailingWageDetermined).NotEmpty();
            RuleFor(p => p.SourceEmployers).NotNull().Must(p => p.Any()).SetCollectionValidator(sourceEmployerValidator);
        }
    }
}
