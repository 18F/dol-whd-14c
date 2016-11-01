using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WIOAWorkerValidator : BaseValidator<WIOAWorker>, IWIOAWorkerValidator
    {
        public WIOAWorkerValidator()
        {
            RuleFor(w => w.FullName).NotEmpty();
            RuleFor(w => w.WIOAWorkerVerifiedId).NotNull().GreaterThanOrEqualTo(39).LessThanOrEqualTo(41);
        }
    }
}
