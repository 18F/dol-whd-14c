using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class EmployeeValidatorTests
    {
        private readonly IEmployeeValidator _employeeValidator;

        public EmployeeValidatorTests()
        {
            _employeeValidator = new EmployeeValidator();
        }

        [TestMethod]
        public void Should_Require_Name()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.Name, "");
        }

        [TestMethod]
        public void Should_Require_PrimaryDisabilityId()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.PrimaryDisabilityId, null as int?);
        }

        [TestMethod]
        public void Should_Require_WorkType()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.WorkType, "");
        }

        [TestMethod]
        public void Should_Require_NumJobs()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.NumJobs, null as int?);
        }

        [TestMethod]
        public void Should_Require_AvgWeeklyHours()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.AvgWeeklyHours, null as double?);
        }

        [TestMethod]
        public void Should_Require_AvgHourlyEarnings()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.AvgHourlyEarnings, null as double?);
        }

        [TestMethod]
        public void Should_Require_PrevailingWage()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.PrevailingWage, null as double?);
        }

        [TestMethod]
        public void Should_Require_ProductivityMeasure()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.ProductivityMeasure, null as double?);
        }

        [TestMethod]
        public void Should_Require_CommensurateWageRate()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.CommensurateWageRate, "");
        }

        [TestMethod]
        public void Should_Require_TotalHours()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.TotalHours, null as double?);
        }

        [TestMethod]
        public void Should_Require_WorkAtOtherSite()
        {
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.WorkAtOtherSite, null as bool?);
        }

        [TestMethod]
        public void Should_Require_PrimaryDisabilityOther()
        {
            var model = new Employee { PrimaryDisabilityId = 37, PrimaryDisabilityOther = null };
            _employeeValidator.ShouldNotHaveValidationErrorFor(e => e.PrimaryDisabilityOther, model);
            model = new Employee { PrimaryDisabilityId = 38, PrimaryDisabilityOther = null};
            _employeeValidator.ShouldHaveValidationErrorFor(e => e.PrimaryDisabilityOther, model);
        }
    }
}
