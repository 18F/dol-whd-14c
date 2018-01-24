using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WIOAWorkerValidator : BaseValidator<WIOAWorker>, IWIOAWorkerValidator
    {
        public WIOAWorkerValidator()
        {
            RuleFor(w => w.FirstName).NotEmpty();
            RuleFor(w => w.LastName).NotEmpty();
            RuleFor(w => w.WIOAWorkerVerifiedId).NotNull().InclusiveBetween(ResponseIds.WIOAWorkerVerified.Yes, ResponseIds.WIOAWorkerVerified.NotRequired);
        }
    }
}
