using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class PieceRateWageInfoValidatorTests
    {
        private static readonly IAddressValidator AddressValidator = new AddressValidator();
        private static readonly ISourceEmployerValidator SourceEmployerValidator = new SourceEmployerValidator(AddressValidator);
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
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageMethodId, 25);
        }

        [TestMethod]
        public void Should_Require_AttachmentId()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AttachmentId, null as Guid?);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AttachmentId, Guid.NewGuid());
        }

        [TestMethod]
        public void Should_Require_MostRecentPrevailingWageSurvey()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = 25, MostRecentPrevailingWageSurvey = null };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 24, MostRecentPrevailingWageSurvey = null };
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 24, MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo() };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
        }

        [TestMethod]
        public void Should_Require_AlternateWageData()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = 24, AlternateWageData = null };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 25, AlternateWageData = null };
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 25, AlternateWageData = new AlternateWageData() };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
        }

        [TestMethod]
        public void Should_Require_SCAWageDeterminationId()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = 24, SCAWageDeterminationId = null };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAWageDeterminationId, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 26, SCAWageDeterminationId = null };
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.SCAWageDeterminationId, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 26, SCAWageDeterminationId = Guid.NewGuid() };
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAWageDeterminationId, model);
        }

        [TestMethod]
        public void Should_Validate_PrevailingWageMethod()
        {
            PieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, 28);
            PieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageMethodId, 25);
        }
    }
}
