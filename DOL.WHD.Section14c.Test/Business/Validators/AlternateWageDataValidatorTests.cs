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
        }

        [TestMethod]
        public void Should_Require_AlternateWorkDescription()
        {
            _alternateWageDataValidator.ShouldHaveValidationErrorFor(a => a.AlternateWorkDescription, "");
        }

        [TestMethod]
        public void Should_Require_DataRetrieved()
        {
            _alternateWageDataValidator.ShouldHaveValidationErrorFor(a => a.DataRetrieved, default(DateTime));
        }

        [TestMethod]
        public void Should_Require_PrevailingWageProvidedBySource()
        {
            _alternateWageDataValidator.ShouldHaveValidationErrorFor(a => a.PrevailingWageProvidedBySource, null as int?);
        }
    }
}
