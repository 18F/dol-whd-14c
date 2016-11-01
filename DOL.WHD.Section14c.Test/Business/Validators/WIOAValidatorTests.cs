using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WIOAValidatorTests
    {
        private readonly IWIOAValidator _wioaValidator;

        public WIOAValidatorTests()
        {
            var wioaWorkerValidator = new Mock<IWIOAWorkerValidator>();
            _wioaValidator = new WIOAValidator(wioaWorkerValidator.Object);
        }

        [TestMethod]
        public void Should_Require_HasVerifiedDocumentation()
        {
            _wioaValidator.ShouldHaveValidationErrorFor(x => x.HasVerifiedDocumentation, null as bool?);
        }

        [TestMethod]
        public void Should_Require_HasWIOAWorkers()
        {
            _wioaValidator.ShouldHaveValidationErrorFor(x => x.HasWIOAWorkers, null as bool?);
        }

        [TestMethod]
        public void Should_Require_WIOAWorkers()
        {
            var model = new WIOA {HasWIOAWorkers = true, WIOAWorkers = null};
            _wioaValidator.ShouldHaveValidationErrorFor(x => x.WIOAWorkers, model);
            model = new WIOA { HasWIOAWorkers = false, WIOAWorkers = null };
            _wioaValidator.ShouldNotHaveValidationErrorFor(x => x.WIOAWorkers, model);
        }
    }
}
