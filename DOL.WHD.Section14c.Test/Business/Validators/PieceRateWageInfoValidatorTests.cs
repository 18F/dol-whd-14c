using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class PieceRateWageInfoValidatorTests
    {
        private readonly IPieceRateWageInfoValidator _pieceRateWageInfoValidator;

        public PieceRateWageInfoValidatorTests()
        {
            var prevailingWageSurveyInfoValidator = new Mock<IPrevailingWageSurveyInfoValidator>();
            var alternateWageDataValidator = new Mock<IAlternateWageDataValidator>();
            _pieceRateWageInfoValidator = new PieceRateWageInfoValidator(prevailingWageSurveyInfoValidator.Object, alternateWageDataValidator.Object);
        }

        [TestMethod]
        public void Should_Require_PieceRateWorkDescription()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PieceRateWorkDescription, "");
        }

        [TestMethod]
        public void Should_Require_PrevailingWageDeterminedForJob()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageDeterminedForJob, null as double?);
        }

        [TestMethod]
        public void Should_Require_StandardProductivity()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.StandardProductivity, null as double?);
        }

        [TestMethod]
        public void Should_Require_PieceRatePaidToWorkers()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PieceRatePaidToWorkers, null as double?);
        }

        // WageTypeInfo
        [TestMethod]
        public void Should_Require_NumWorkers()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.NumWorkers, null as int?);
        }

        [TestMethod]
        public void Should_Require_JobName()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobName, "");
        }

        [TestMethod]
        public void Should_Require_JobDescription()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.JobDescription, "");
        }

        [TestMethod]
        public void Should_Require_PrevailingWageMethodId()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageMethodId, null as int?);
        }

        [TestMethod]
        public void Should_Require_AttachmentId()
        {
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AttachmentId, null as Guid?);
        }

        [TestMethod]
        public void Should_Require_MostRecentPrevailingWageSurvey()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = 25, MostRecentPrevailingWageSurvey = null };
            _pieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 24, MostRecentPrevailingWageSurvey = null };
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.MostRecentPrevailingWageSurvey, model);
        }

        [TestMethod]
        public void Should_Require_AlternateWageData()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = 24, AlternateWageData = null };
            _pieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.AlternateWageData, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 25, AlternateWageData = null };
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.AlternateWageData, model);
        }

        [TestMethod]
        public void Should_Require_SCAWageDeterminationId()
        {
            var model = new PieceRateWageInfo { PrevailingWageMethodId = 24, SCAWageDeterminationId = null };
            _pieceRateWageInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SCAWageDeterminationId, model);
            model = new PieceRateWageInfo { PrevailingWageMethodId = 26, SCAWageDeterminationId = null };
            _pieceRateWageInfoValidator.ShouldHaveValidationErrorFor(x => x.SCAWageDeterminationId, model);
        }
    }
}
