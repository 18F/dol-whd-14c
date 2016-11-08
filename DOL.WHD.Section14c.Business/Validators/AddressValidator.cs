using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class AddressValidator : AddressValidatorNoCounty, IAddressValidator
    {
        public AddressValidator()
        {
            RuleFor(a => a.County).NotEmpty();
        }
    }
}
