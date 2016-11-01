using DOL.WHD.Section14c.Business.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WorkerCountInfoValidatorTests
    {
        private readonly IWorkerCountInfoValidator _workerCountInfoValidator;

        public WorkerCountInfoValidatorTests()
        {
            _workerCountInfoValidator = new WorkerCountInfoValidator();
        }

        [TestMethod]
        public void Should_Require_Total()
        {
            _workerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.Total, null as int?);
        }

        [TestMethod]
        public void Should_Require_WorkCenter()
        {
            _workerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.WorkCenter, null as int?);
        }

        [TestMethod]
        public void Should_Require_PatientWorkers()
        {
            _workerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.PatientWorkers, null as int?);
        }

        [TestMethod]
        public void Should_Require_SWEP()
        {
            _workerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.SWEP, null as int?);
        }

        [TestMethod]
        public void Should_Require_BusinessEstablishment()
        {
            _workerCountInfoValidator.ShouldHaveValidationErrorFor(x => x.BusinessEstablishment, null as int?);
        }
    }
}
