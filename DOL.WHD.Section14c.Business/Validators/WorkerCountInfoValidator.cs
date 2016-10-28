using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WorkerCountInfoValidator : AbstractValidator<WorkerCountInfo>, IWorkerCountInfoValidator
    {
        public WorkerCountInfoValidator()
        {
            RuleFor(w => w.Total).NotEmpty();
            RuleFor(a => a.WorkCenter).NotNull();
            RuleFor(a => a.PatientWorkers).NotNull();
            RuleFor(a => a.SWEP).NotNull();
            RuleFor(a => a.BusinessEstablishment).NotNull();
        }
    }
}
