using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WIOAWorkerValidatorTests
    {
        private readonly IWIOAWorkerValidator _wioaWorkerValidator;

        public WIOAWorkerValidatorTests()
        {
            _wioaWorkerValidator = new WIOAWorkerValidator();
        }

        [TestMethod]
        public void Should_Require_FullName()
        {
            _wioaWorkerValidator.ShouldHaveValidationErrorFor(x => x.FullName, "");
        }

        [TestMethod]
        public void Should_Require_WIOAWorkerVerifiedId()
        {
            _wioaWorkerValidator.ShouldHaveValidationErrorFor(x => x.WIOAWorkerVerifiedId, null as int?);
        }
    }
}
