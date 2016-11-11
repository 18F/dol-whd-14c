using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WIOAWorkerValidatorTests
    {
        private static readonly IWIOAWorkerValidator WIOAWorkerValidator = new WIOAWorkerValidator();

        [TestMethod]
        public void Should_Require_FullName()
        {
            WIOAWorkerValidator.ShouldHaveValidationErrorFor(x => x.FullName, "");
            WIOAWorkerValidator.ShouldNotHaveValidationErrorFor(x => x.FullName, "Full Name");
        }

        [TestMethod]
        public void Should_Require_WIOAWorkerVerifiedId()
        {
            WIOAWorkerValidator.ShouldHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, null as int?);
            WIOAWorkerValidator.ShouldNotHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, 39);
        }

        [TestMethod]
        public void Should_Validate_WIOAWorkerVerified()
        {
            WIOAWorkerValidator.ShouldHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, 42);
            WIOAWorkerValidator.ShouldNotHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, 39);
        }
    }
}
