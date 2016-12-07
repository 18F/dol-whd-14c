using System;
using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class SignatureValidatorTests
    {
        private ISignatureValidator _signatureValidator;
        public SignatureValidatorTests()
        {
            _signatureValidator = new SignatureValidator();
        }

        [TestMethod]
        public void Should_Require_Agreement()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.Agreement, null as bool?);
        }

        [TestMethod]
        public void Should_Require_FullName()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.FullName, "");
        }

        [TestMethod]
        public void Should_Require_Title()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.Title, "");
        }

        [TestMethod]
        public void Should_Require_Date()
        {
            _signatureValidator.ShouldHaveValidationErrorFor(x => x.Date, default(DateTime));
        }
    }
}
