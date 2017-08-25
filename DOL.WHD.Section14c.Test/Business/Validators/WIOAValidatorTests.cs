using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WIOAValidatorTests
    {
        private static readonly IWIOAWorkerValidator WIOAWorkerValidator = new WIOAWorkerValidator();
        private static readonly IWIOAValidator WIOAValidator = new WIOAValidator(WIOAWorkerValidator);

        [TestMethod]
        public void Should_Require_HasVerifiedDocumentation()
        {
            WIOAValidator.ShouldHaveValidationErrorFor(x => x.HasVerifiedDocumentation, null as bool?);
            WIOAValidator.ShouldNotHaveValidationErrorFor(x => x.HasVerifiedDocumentation, false);
        }

        [TestMethod]
        public void Should_Require_HasWIOAWorkers()
        {
            WIOAValidator.ShouldHaveValidationErrorFor(x => x.HasWIOAWorkers, null as bool?);
            WIOAValidator.ShouldNotHaveValidationErrorFor(x => x.HasWIOAWorkers, false);
        }

        [TestMethod]
        public void Should_Require_WIOAWorkers()
        {
            var model = new WIOA {HasWIOAWorkers = true, WIOAWorkers = null};
            WIOAValidator.ShouldHaveValidationErrorFor(x => x.WIOAWorkers, model);
            model = new WIOA { HasWIOAWorkers = false, WIOAWorkers = null };
            WIOAValidator.ShouldNotHaveValidationErrorFor(x => x.WIOAWorkers, model);
            model = new WIOA {HasWIOAWorkers = true, WIOAWorkers = new List<WIOAWorker> {new WIOAWorker()}};
            WIOAValidator.ShouldNotHaveValidationErrorFor(x => x.WIOAWorkers, model);
        }
    }
}
