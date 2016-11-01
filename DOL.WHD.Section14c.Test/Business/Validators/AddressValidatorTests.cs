using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class AddressValidatorTests
    {
        private readonly IAddressValidator _addressValidator;

        public AddressValidatorTests()
        {
            _addressValidator = new AddressValidator();
        }

        [TestMethod]
        public void Should_Require_StreetAddress()
        {
            _addressValidator.ShouldHaveValidationErrorFor(a => a.StreetAddress, "");
        }

        [TestMethod]
        public void Should_Require_City()
        {
            _addressValidator.ShouldHaveValidationErrorFor(a => a.City, "");
        }

        [TestMethod]
        public void Should_Require_State()
        {
            _addressValidator.ShouldHaveValidationErrorFor(a => a.State, "");
        }

        [TestMethod]
        public void Should_Require_ZipCode()
        {
            _addressValidator.ShouldHaveValidationErrorFor(a => a.ZipCode, "");
        }

        [TestMethod]
        public void Should_Require_County()
        {
            _addressValidator.ShouldHaveValidationErrorFor(a => a.County, "");
        }
    }
}
