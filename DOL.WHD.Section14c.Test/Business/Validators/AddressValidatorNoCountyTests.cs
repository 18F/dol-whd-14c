using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class AddressValidatorNoCountyTests
    {
        private readonly IAddressValidatorNoCounty _addressValidatorNoCounty;

        public AddressValidatorNoCountyTests()
        {
            _addressValidatorNoCounty = new AddressValidatorNoCounty();
        }

        [TestMethod]
        public void Should_Require_StreetAddress()
        {
            _addressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.StreetAddress, "");
        }

        [TestMethod]
        public void Should_Require_City()
        {
            _addressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.City, "");
        }

        [TestMethod]
        public void Should_Require_State()
        {
            _addressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.State, "");
        }

        [TestMethod]
        public void Should_Require_ZipCode()
        {
            _addressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.ZipCode, "");
        }

        [TestMethod]
        public void Should_Not_Require_County()
        {
            _addressValidatorNoCounty.ShouldNotHaveValidationErrorFor(a => a.County, "");
        }
    }
}
