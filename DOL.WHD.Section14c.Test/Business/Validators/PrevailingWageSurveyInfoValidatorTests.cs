using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class PrevailingWageSurveyInfoValidatorTests
    {
        private static readonly IAddressValidator AddressValidator = new AddressValidator();
        private static readonly ISourceEmployerValidator SourceEmployerValidator = new SourceEmployerValidator(AddressValidator);
        private static readonly IPrevailingWageSurveyInfoValidator PrevailingWageSurveyInfoValidator = new PrevailingWageSurveyInfoValidator(SourceEmployerValidator);

        [TestMethod]
        public void Should_Require_PrevailingWageDetermined()
        {
            PrevailingWageSurveyInfoValidator.ShouldHaveValidationErrorFor(x => x.PrevailingWageDetermined, null as double?);
            PrevailingWageSurveyInfoValidator.ShouldNotHaveValidationErrorFor(x => x.PrevailingWageDetermined, 15.55);
        }

        [TestMethod]
        public void Should_Require_SourceEmployers()
        {
            PrevailingWageSurveyInfoValidator.ShouldHaveValidationErrorFor(x => x.SourceEmployers, null as ICollection<SourceEmployer>);
            PrevailingWageSurveyInfoValidator.ShouldHaveValidationErrorFor(x => x.SourceEmployers, new List<SourceEmployer>());
            PrevailingWageSurveyInfoValidator.ShouldNotHaveValidationErrorFor(x => x.SourceEmployers, new List<SourceEmployer> {new SourceEmployer()});
        }
    }
}
