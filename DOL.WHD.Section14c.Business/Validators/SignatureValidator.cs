using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class SignatureValidator : AbstractValidator<Signature>, ISignatureValidator
    {
        public SignatureValidator()
        {
            RuleFor(s => s.Agreement).Equal(true);
            RuleFor(s => s.FullName).NotEmpty();
            RuleFor(s => s.Title).NotEmpty();
            RuleFor(s => s.Date).NotEmpty();
        }
    }
}
