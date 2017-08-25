using System;
using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class SignatureValidatorTests
    {
        private readonly ISignatureValidator _signatureValidator;
        public SignatureValidatorTests()
        {
            _signatureValidator = new SignatureValidator();
        }

        [TestMethod]
        public void Should_Require_Agreement()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.Agreement, null as bool?);
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.Agreement, false);
            _signatureValidator.ShouldNotHaveValidationErrorFor(x => x.Agreement, true);
        }

        [TestMethod]
        public void Should_Require_FullName()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.FullName, "");
            _signatureValidator.ShouldNotHaveValidationErrorFor(x => x.FullName, "Full Name");
        }

        [TestMethod]
        public void Should_Require_Title()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.Title, "");
            _signatureValidator.ShouldNotHaveValidationErrorFor(x => x.Title, "Title");
        }

        [TestMethod]
        public void Should_Require_Date()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.Date, default(DateTime));
            _signatureValidator.ShouldNotHaveValidationErrorFor(x => x.Date, DateTime.Now);
        }
    }
}
