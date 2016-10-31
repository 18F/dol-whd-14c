using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class PieceRateWageInfoValidator : WageTypeInfoValidator<PieceRateWageInfo>, IPieceRateWageInfoValidator
    {
        public PieceRateWageInfoValidator(IPrevailingWageSurveyInfoValidator prevailingWageSurveyInfoValidator,
            IAlternateWageDataValidator alternateWageDataValidator)
            : base(prevailingWageSurveyInfoValidator, alternateWageDataValidator)
        {
            RuleFor(p => p.PieceRateWorkDescription).NotEmpty();
            RuleFor(p => p.PrevailingWageDeterminedForJob).NotNull();
            RuleFor(p => p.StandardProductivity).NotNull();
            RuleFor(p => p.PieceRatePaidToWorkers).NotNull();
        }
    }
}
