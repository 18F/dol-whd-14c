using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        public BaseValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
