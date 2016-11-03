using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class HourlyWageInfoValidatorTests
    {
        private readonly IHourlyWageInfoValidator _hourlyWageInfoValidator;

        public HourlyWageInfoValidatorTests()
        {
            var prevailingWageSurveyInfoValidator = new Mock<IPrevailingWageSurveyInfoValidator>();
            var alternateWageDataValidator = new Mock<IAlternateWageDataValidator>();
            _hourlyWageInfoValidator = new HourlyWageInfoValidator(prevailingWageSurveyInfoValidator.Object, alternateWageDataValidator.Object);
        }

        [TestMethod]
        public void Should_Require_WorkMeasurementFrequency()
        {
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.WorkMeasurementFrequency, "");
        }

        // WageTypeInfo
        [TestMethod]
        public void Should_Require_NumWorkers()
        {
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.NumWorkers, null as int?);
        }

        [TestMethod]
        public void Should_Require_JobName()
        {
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobName, "");
        }

        [TestMethod]
        public void Should_Require_JobDescription()
        {
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobDescription, "");
        }

        [TestMethod]
        public void Should_Require_PrevailingWageMethodId()
        {
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, null as int?);
        }

        [TestMethod]
        public void Should_Require_AttachmentId()
        {
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AttachmentId, null as Guid?);
        }

        [TestMethod]
        public void Should_Require_MostRecentPrevailingWageSurvey()
        {
            var model = new HourlyWageInfo { PrevailingWageMethodId = 25, MostRecentPrevailingWageSurvey = null };
            _hourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new HourlyWageInfo {PrevailingWageMethodId = 24, MostRecentPrevailingWageSurvey = null};
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
        }

        [TestMethod]
        public void Should_Require_AlternateWageData()
        {
            var model = new HourlyWageInfo { PrevailingWageMethodId = 24, AlternateWageData = null };
            _hourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new HourlyWageInfo { PrevailingWageMethodId = 25, AlternateWageData = null };
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AlternateWageData, model);
        }

        [TestMethod]
        public void Should_Require_SCAWageDeterminationId()
        {
            var model = new HourlyWageInfo { PrevailingWageMethodId = 24, SCAWageDeterminationId = null };
            _hourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAWageDeterminationId, model);
            model = new HourlyWageInfo { PrevailingWageMethodId = 26, SCAWageDeterminationId = null };
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.SCAWageDeterminationId, model);
        }

        [TestMethod]
        public void Should_Validate_PrevailingWageMethod()
        {
            _hourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, 28);
            _hourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageMethodId, 25);
        }
    }
}
