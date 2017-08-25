using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WorkerCountInfoValidatorTests
    {
        private static readonly IWorkerCountInfoValidator WorkerCountInfoValidator = new WorkerCountInfoValidator();

        [TestMethod]
        public void Should_Require_Total()
        {
            WorkerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.Total, null as int?);
            WorkerCountInfoValidator.ShouldNotHaveValidationErrorFor(x => x.Total, 6);
        }

        [TestMethod]
        public void Should_Require_WorkCenter()
        {
            WorkerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.WorkCenter, null as int?);
            WorkerCountInfoValidator.ShouldNotHaveValidationErrorFor(x => x.WorkCenter, 3);
        }

        [TestMethod]
        public void Should_Require_PatientWorkers()
        {
            WorkerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.PatientWorkers, null as int?);
            WorkerCountInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PatientWorkers, 2);
        }

        [TestMethod]
        public void Should_Require_SWEP()
        {
            WorkerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.SWEP, null as int?);
            WorkerCountInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SWEP, 4);
        }

        [TestMethod]
        public void Should_Require_BusinessEstablishment()
        {
            WorkerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.BusinessEstablishment, null as int?);
            WorkerCountInfoValidator.ShouldNotHaveValidationErrorFor(x => x.BusinessEstablishment, 0);
        }
    }
}
