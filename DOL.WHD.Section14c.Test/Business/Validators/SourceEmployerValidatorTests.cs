using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class SourceEmployerValidatorTests
    {
        private readonly ISourceEmployerValidator _sourceEmployerValidator;

        public SourceEmployerValidatorTests()
        {
            var addressValidator = new Mock<IAddressValidator>();
            _sourceEmployerValidator = new SourceEmployerValidator(addressValidator.Object);
        }

        [TestMethod]
        public void Should_Require_EmployerName()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.EmployerName, "");
        }

        [TestMethod]
        public void Should_Require_Address()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.Address, null as Address);
        }

        [TestMethod]
        public void Should_Require_Phone()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.Phone, "");
        }

        [TestMethod]
        public void Should_Require_ContactName()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ContactName, "");
        }

        [TestMethod]
        public void Should_Require_ContactTitle()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ContactTitle, "");
        }

        [TestMethod]
        public void Should_Require_ContactDate()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ContactDate, default(DateTime));
        }

        [TestMethod]
        public void Should_Require_JobDescription()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.JobDescription, "");
        }

        [TestMethod]
        public void Should_Require_ExperiencedWorkerWageProvided()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ExperiencedWorkerWageProvided, "");
        }

        [TestMethod]
        public void Should_Require_ConclusionWageRateNotBasedOnEntry()
        {
            _sourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ConclusionWageRateNotBasedOnEntry, "");
        }
    }
}
