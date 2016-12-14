using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class SourceEmployerValidatorTests
    {
        private static readonly IAddressValidatorNoCounty AddressValidatorNoCounty = new AddressValidatorNoCounty();
        private static readonly ISourceEmployerValidator SourceEmployerValidator = new SourceEmployerValidator(AddressValidatorNoCounty);

        [TestMethod]
        public void Should_Require_EmployerName()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.EmployerName, "");
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.EmployerName, "Employer Name");
        }

        [TestMethod]
        public void Should_Require_Address()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.Address, null as Address);
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.Address, new Address());
        }

        [TestMethod]
        public void Should_Require_Phone()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.Phone, "");
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.Phone, "123-456-7890");
        }

        [TestMethod]
        public void Should_Require_ContactName()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ContactName, "");
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ContactName, "Contact Name");
        }

        [TestMethod]
        public void Should_Require_ContactTitle()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ContactTitle, "");
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ContactTitle, "Contact Title");
        }

        [TestMethod]
        public void Should_Require_ContactDate()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ContactDate, default(DateTime));
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ContactDate, DateTime.Now);
        }

        [TestMethod]
        public void Should_Require_JobDescription()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.JobDescription, "");
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.JobDescription, "Job Description");
        }

        [TestMethod]
        public void Should_Require_ExperiencedWorkerWageProvided()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ExperiencedWorkerWageProvided, "");
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ExperiencedWorkerWageProvided, "Experienced Worker Wage Provided");
        }

        [TestMethod]
        public void Should_Require_ConclusionWageRateNotBasedOnEntry()
        {
            SourceEmployerValidator.ShouldHaveValidationErrorFor(x => x.ConclusionWageRateNotBasedOnEntry, "");
            SourceEmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ConclusionWageRateNotBasedOnEntry, "Conclusion Wage Rate Not Based On Entry");
        }
    }
}
