using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class AddressValidatorTests
    {
        private static readonly IAddressValidator AddressValidator = new AddressValidator();

        [TestMethod]
        public void Should_Require_StreetAddress()
        {
            AddressValidator.ShouldHaveValidationErrorFor(a => a.StreetAddress, "");
            AddressValidator.ShouldNotHaveValidationErrorFor(a => a.StreetAddress, "1600 Pennsylvania Ave NW");
        }

        [TestMethod]
        public void Should_Require_City()
        {
            AddressValidator.ShouldHaveValidationErrorFor(a => a.City, "");
            AddressValidator.ShouldNotHaveValidationErrorFor(a => a.City, "Washington");
        }

        [TestMethod]
        public void Should_Require_State()
        {
            AddressValidator.ShouldHaveValidationErrorFor(a => a.State, "");
            AddressValidator.ShouldNotHaveValidationErrorFor(a => a.State, "DC");
        }

        [TestMethod]
        public void Should_Require_ZipCode()
        {
            AddressValidator.ShouldHaveValidationErrorFor(a => a.ZipCode, "");
            AddressValidator.ShouldNotHaveValidationErrorFor(a => a.ZipCode, "20500");
        }

        [TestMethod]
        public void Should_Require_County()
        {
            AddressValidator.ShouldHaveValidationErrorFor(a => a.County, "");
            AddressValidator.ShouldNotHaveValidationErrorFor(a => a.County, "Washington");
        }
    }
}
