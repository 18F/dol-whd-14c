using System;
using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class AlternateWageDataValidatorTests
    {
        private readonly IAlternateWageDataValidator _alternateWageDataValidator;

        public AlternateWageDataValidatorTests()
        {
            _alternateWageDataValidator = new AlternateWageDataValidator();
        }

        [TestMethod]
        public void Should_Require_AlternateDataSourceUsed()
        {
            _alternateWageDataValidator.ShouldHaveValidationErrorFor(a => a.AlternateDataSourceUsed, "");
            _alternateWageDataValidator.ShouldNotHaveValidationErrorFor(a => a.AlternateDataSourceUsed, "Alternate Data Source Used");
        }

        [TestMethod]
        public void Should_Require_AlternateWorkDescription()
        {
            _alternateWageDataValidator.ShouldHaveValidationErrorFor(a => a.AlternateWorkDescription, "");
            _alternateWageDataValidator.ShouldNotHaveValidationErrorFor(a => a.AlternateWorkDescription, "Alternate Work Description");
        }

        [TestMethod]
        public void Should_Require_DataRetrieved()
        {
            _alternateWageDataValidator.ShouldHaveValidationErrorFor(a => a.DataRetrieved, default(DateTime));
            _alternateWageDataValidator.ShouldNotHaveValidationErrorFor(a => a.DataRetrieved, DateTime.Now);
        }

        [TestMethod]
        public void Should_Require_PrevailingWageProvidedBySource()
        {
            _alternateWageDataValidator.ShouldHaveValidationErrorFor(a => a.PrevailingWageProvidedBySource, null as double?);
            _alternateWageDataValidator.ShouldNotHaveValidationErrorFor(a => a.PrevailingWageProvidedBySource, 50.15);
        }
    }
}
