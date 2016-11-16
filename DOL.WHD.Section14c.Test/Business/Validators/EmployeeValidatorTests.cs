using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class EmployeeValidatorTests
    {
        private static readonly IEmployeeValidator EmployeeValidator = new EmployeeValidator();

        [TestMethod]
        public void Should_Require_Name()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.Name, "");
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.Name, "Employee Name");
        }

        [TestMethod]
        public void Should_Require_PrimaryDisabilityId()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.PrimaryDisabilityId, null as int?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.PrimaryDisabilityId, ResponseIds.PrimaryDisability.Neuromuscular);
        }

        [TestMethod]
        public void Should_Require_WorkType()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.WorkType, "");
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.WorkType, "Work Type");
        }

        [TestMethod]
        public void Should_Require_NumJobs()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.NumJobs, null as int?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.NumJobs, 20);
        }

        [TestMethod]
        public void Should_Require_AvgWeeklyHours()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.AvgWeeklyHours, null as double?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.AvgWeeklyHours, 20.25);
        }

        [TestMethod]
        public void Should_Require_AvgHourlyEarnings()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.AvgHourlyEarnings, null as double?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.AvgHourlyEarnings, 15.55);
        }

        [TestMethod]
        public void Should_Require_PrevailingWage()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.PrevailingWage, null as double?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.PrevailingWage, 10.56);
        }

        [TestMethod]
        public void Should_Require_ProductivityMeasure()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.ProductivityMeasure, null as double?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.ProductivityMeasure, 15.32);
        }

        [TestMethod]
        public void Should_Require_CommensurateWageRate()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.CommensurateWageRate, "");
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.CommensurateWageRate, "CommensurateWageRate");
        }

        [TestMethod]
        public void Should_Require_TotalHours()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.TotalHours, null as double?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.TotalHours, 20.55);
        }

        [TestMethod]
        public void Should_Require_WorkAtOtherSite()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.WorkAtOtherSite, null as bool?);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.WorkAtOtherSite, false);
        }

        [TestMethod]
        public void Should_Require_PrimaryDisabilityOther()
        {
            var model = new Employee { PrimaryDisabilityId = ResponseIds.PrimaryDisability.SubstanceAbuse, PrimaryDisabilityOther = null };
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.PrimaryDisabilityOther, model);
            model = new Employee { PrimaryDisabilityId = ResponseIds.PrimaryDisability.Other, PrimaryDisabilityOther = null};
            EmployeeValidator.ShouldHaveValidationErrorFor(e => e.PrimaryDisabilityOther, model);
            model = new Employee { PrimaryDisabilityId = ResponseIds.PrimaryDisability.Other, PrimaryDisabilityOther = "Other" };
            EmployeeValidator.ShouldNotHaveValidationErrorFor(e => e.PrimaryDisabilityOther, model);
        }

        [TestMethod]
        public void Should_Validate_PrimaryDisability()
        {
            EmployeeValidator.ShouldHaveValidationErrorFor(x => x.PrimaryDisabilityId, 40);
            EmployeeValidator.ShouldNotHaveValidationErrorFor(x => x.PrimaryDisabilityId, ResponseIds.PrimaryDisability.IntellectualDevelopmental);
        }
    }
}
