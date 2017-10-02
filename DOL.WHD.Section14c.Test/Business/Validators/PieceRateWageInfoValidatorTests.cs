using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class PieceRateWageInfoValidatorTests
    {
        private static readonly IAddressValidatorNoCounty AddressValidatorNoCounty = new AddressValidatorNoCounty();
        private static readonly ISourceEmployerValidator SourceEmployerValidator = new SourceEmployerValidator(AddressValidatorNoCounty);
        private static readonly IPrevailingWageSurveyInfoValidator PrevailingWageSurveyInfoValidator = new PrevailingWageSurveyInfoValidator(SourceEmployerValidator);
        private static readonly IAlternateWageDataValidator AlternateWageDataValidator = new AlternateWageDataValidator();
        private static readonly IPieceRateWageInfoValidator PieceRateWageInfoValidator = new PieceRateWageInfoValidator(PrevailingWageSurveyInfoValidator, AlternateWageDataValidator);

        [TestMethod]
        public void Should_Require_PieceRateWorkDescription()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PieceRateWorkDescription, "");
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PieceRateWorkDescription, "Piece Rate Work Description");
        }

        [TestMethod]
        public void Should_Require_PrevailingWageDeterminedForJob()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageDeterminedForJob, null as double?);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageDeterminedForJob, 100.56);
        }

        [TestMethod]
        public void Should_Require_StandardProductivity()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.StandardProductivity, null as double?);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.StandardProductivity, 125.45);
        }

        [TestMethod]
        public void Should_Require_PieceRatePaidToWorkers()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PieceRatePaidToWorkers, null as double?);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PieceRatePaidToWorkers, 45.55);
        }

        // WageTypeInfo
        [TestMethod]
        public void Should_Require_NumWorkers()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.NumWorkers, null as int?);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.NumWorkers, 5);
        }

        [TestMethod]
        public void Should_Require_JobName()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobName, "");
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.JobName, "Job Name");
        }

        [TestMethod]
        public void Should_Require_JobDescription()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobDescription, "");
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.JobDescription, "Job Description");
        }

        [TestMethod]
        public void Should_Require_PrevailingWageMethodId()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, null as int?);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageMethodId, ResponseIds.PrevailingWageMethod.AlternateWageData);
        }

        [TestMethod]
        public void Should_Require_AttachmentId()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AttachmentId, null as String);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AttachmentId, Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void Should_Require_MostRecentPrevailingWageSurvey()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.AlternateWageData, MostRecentPrevailingWageSurvey = null };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, MostRecentPrevailingWageSurvey = null };
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo() };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
        }

        [TestMethod]
        public void Should_Require_AlternateWageData()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, AlternateWageData = null };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.AlternateWageData, AlternateWageData = null };
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.AlternateWageData, AlternateWageData = new AlternateWageData() };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
        }

        [TestMethod]
        public void Should_Require_SCAWageDeterminationId()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, SCAWageDeterminationAttachmentId = null };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAWageDeterminationAttachmentId, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.SCAWageDetermination, SCAWageDeterminationAttachmentId = null };
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.SCAWageDeterminationAttachmentId, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.SCAWageDetermination, SCAWageDeterminationAttachmentId = Guid.NewGuid().ToString() };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAWageDeterminationAttachmentId, model);
        }

        [TestMethod]
        public void Should_Validate_PrevailingWageMethod()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, 28);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageMethodId, ResponseIds.PrevailingWageMethod.AlternateWageData);
        }
    }
}
