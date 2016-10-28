using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WIOAValidator : AbstractValidator<WIOA>, IWIOAValidator
    {
        public WIOAValidator(IWIOAWorkerValidator wioaWorkerValidator)
        {
            RuleFor(w => w.HasVerfiedDocumentaion).NotNull();
            RuleFor(w => w.HasWIOAWorkers).NotNull();

            RuleFor(w => w.WIOAWorkers)
                .NotNull()
                .Must(w => w.Any())
                .SetCollectionValidator(wioaWorkerValidator)
                .When(w => w.HasWIOAWorkers.GetValueOrDefault());
        }
    }
}
