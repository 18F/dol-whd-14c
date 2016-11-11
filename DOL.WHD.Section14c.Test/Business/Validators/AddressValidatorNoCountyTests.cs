using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class AddressValidatorNoCountyTests
    {
        private static readonly IAddressValidatorNoCounty AddressValidatorNoCounty = new AddressValidatorNoCounty();

        [TestMethod]
        public void Should_Require_StreetAddress()
        {
            AddressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.StreetAddress, "");
            AddressValidatorNoCounty.ShouldNotHaveValidationErrorFor(a => a.StreetAddress, "1600 Pennsylvania Ave NW");
        }

        [TestMethod]
        public void Should_Require_City()
        {
            AddressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.City, "");
            AddressValidatorNoCounty.ShouldNotHaveValidationErrorFor(a => a.City, "Washington");
        }

        [TestMethod]
        public void Should_Require_State()
        {
            AddressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.State, "");
            AddressValidatorNoCounty.ShouldNotHaveValidationErrorFor(a => a.State, "DC");
        }

        [TestMethod]
        public void Should_Require_ZipCode()
        {
            AddressValidatorNoCounty.ShouldHaveValidationErrorFor(a => a.ZipCode, "");
            AddressValidatorNoCounty.ShouldNotHaveValidationErrorFor(a => a.ZipCode, "20500");
        }

        [TestMethod]
        public void Should_Not_Require_County()
        {
            AddressValidatorNoCounty.ShouldNotHaveValidationErrorFor(a => a.County, "");
            AddressValidatorNoCounty.ShouldNotHaveValidationErrorFor(a => a.County, "Washington");
        }
    }
}
