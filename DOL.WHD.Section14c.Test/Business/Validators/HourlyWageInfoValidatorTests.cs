using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class HourlyWageInfoValidatorTests
    {
        private static readonly IAddressValidatorNoCounty AddressValidatorNoCounty = new AddressValidatorNoCounty();
        private static readonly ISourceEmployerValidator SourceEmployerValidator = new SourceEmployerValidator(AddressValidatorNoCounty);
        private static readonly IPrevailingWageSurveyInfoValidator PrevailingWageSurveyInfoValidator = new PrevailingWageSurveyInfoValidator(SourceEmployerValidator);
        private static readonly IAlternateWageDataValidator AlternateWageDataValidator = new AlternateWageDataValidator();
        private static readonly IHourlyWageInfoValidator HourlyWageInfoValidator = new HourlyWageInfoValidator(PrevailingWageSurveyInfoValidator, AlternateWageDataValidator);

        [TestMethod]
        public void Should_Require_WorkMeasurementFrequency()
        {
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.WorkMeasurementFrequency, "");
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.WorkMeasurementFrequency, "Work Measurement Frequency");
        }

        // WageTypeInfo
        [TestMethod]
        public void Should_Require_NumWorkers()
        {
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.NumWorkers, null as int?);
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.NumWorkers, 5);
        }

        [TestMethod]
        public void Should_Require_JobName()
        {
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobName, "");
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.JobName, "Job Name");
        }

        [TestMethod]
        public void Should_Require_JobDescription()
        {
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobDescription, "");
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.JobDescription, "Job Description");
        }

        [TestMethod]
        public void Should_Require_PrevailingWageMethodId()
        {
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, null as int?);
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageMethodId, ResponseIds.PrevailingWageMethod.AlternateWageData);
        }

        [TestMethod]
        public void Should_Require_AttachmentId()
        {
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AttachmentId, null as String);
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AttachmentId, Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void Should_Require_MostRecentPrevailingWageSurvey()
        {
            var model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.AlternateWageData, MostRecentPrevailingWageSurvey = null };
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new HourlyWageInfo {PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, MostRecentPrevailingWageSurvey = null};
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo() };
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
        }

        [TestMethod]
        public void Should_Require_AlternateWageData()
        {
            var model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, AlternateWageData = null };
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.AlternateWageData, AlternateWageData = null };
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.AlternateWageData, AlternateWageData = new AlternateWageData() };
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
        }

        [TestMethod]
        public void Should_Require_SCAWageDeterminationId()
        {
            var model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, SCAAttachments = null };
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAAttachments, model);
            model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.SCAWageDetermination, SCAAttachments = null };
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.SCAAttachments, model);
            model = new HourlyWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.SCAWageDetermination, SCAAttachments= new List<WageTypeInfoSCAAttachment>() { new WageTypeInfoSCAAttachment() { WageTypeInfoId = Guid.NewGuid().ToString() } } };
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAAttachments, model);
        }

        [TestMethod]
        public void Should_Validate_PrevailingWageMethod()
        {
            HourlyWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, 28);
            HourlyWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageMethodId, ResponseIds.PrevailingWageMethod.AlternateWageData);
        }
    }
}
