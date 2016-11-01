using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class PrevailingWageSurveyInfoValidatorTests
    {
        private readonly IPrevailingWageSurveyInfoValidator _prevailingWageSurveyInfoValidator;

        public PrevailingWageSurveyInfoValidatorTests()
        {
            var sourceEmployerValidator = new Mock<ISourceEmployerValidator>();
            _prevailingWageSurveyInfoValidator = new PrevailingWageSurveyInfoValidator(sourceEmployerValidator.Object);
        }

        [TestMethod]
        public void Should_Require_PrevailingWageDetermined()
        {
            _prevailingWageSurveyInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageDetermined, null as double?);
        }

        [TestMethod]
        public void Should_Require_SourceEmployers()
        {
            _prevailingWageSurveyInfoValidator.ShouldHaveValidationErrorFor(x => x.SourceEmployers, null as ICollection<SourceEmployer>);
            _prevailingWageSurveyInfoValidator.ShouldHaveValidationErrorFor(x => x.SourceEmployers, new List<SourceEmployer>());
        }
    }
}
