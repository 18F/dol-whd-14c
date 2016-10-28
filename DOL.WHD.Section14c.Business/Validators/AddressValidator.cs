using DOL.WHD.Section14c.Domain.Models;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class AddressValidator : AbstractValidator<Address>, IAddressValidator
    {
        public AddressValidator()
        {
            RuleFor(a => a.StreetAddress).NotEmpty();
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.State).NotEmpty();
            RuleFor(a => a.ZipCode).NotEmpty();
            RuleFor(a => a.County).NotEmpty();
        }
    }
}
