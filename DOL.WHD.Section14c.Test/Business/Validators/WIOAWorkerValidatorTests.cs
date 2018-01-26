using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
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
            WIOAWorkerValidator.ShouldHaveValidationErrorFor(x => x.FirstName, "");
            WIOAWorkerValidator.ShouldNotHaveValidationErrorFor(x => x.FirstName, "First Name");
            WIOAWorkerValidator.ShouldHaveValidationErrorFor(x => x.LastName, "");
            WIOAWorkerValidator.ShouldNotHaveValidationErrorFor(x => x.LastName, "Last Name");
        }

        [TestMethod]
        public void Should_Require_WIOAWorkerVerifiedId()
        {
            WIOAWorkerValidator.ShouldHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, null as int?);
            WIOAWorkerValidator.ShouldNotHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, ResponseIds.WIOAWorkerVerified.Yes);
        }

        [TestMethod]
        public void Should_Validate_WIOAWorkerVerified()
        {
            WIOAWorkerValidator.ShouldHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, 42);
            WIOAWorkerValidator.ShouldNotHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, ResponseIds.WIOAWorkerVerified.Yes);
        }
    }
}
