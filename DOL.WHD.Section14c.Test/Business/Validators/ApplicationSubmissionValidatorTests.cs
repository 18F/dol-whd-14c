using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class ApplicationSubmissionValidatorTests
    {
        private readonly ApplicationSubmissionValidator _applicationSubmissionValidator;
        public ApplicationSubmissionValidatorTests()
        {
            var employerValidatorMock = new Mock<IEmployerValidator>();
            var hourlyWageInfoValidatorMock = new Mock<IHourlyWageInfoValidator>();
            var pieceRateWageInfoValidatorMock = new Mock<IPieceRateWageInfoValidator>();
            var workSiteValidatorMock = new Mock<IWorkSiteValidator>();
            var wioaValidatorMock = new Mock<IWIOAValidator>();
            _applicationSubmissionValidator = new ApplicationSubmissionValidator(employerValidatorMock.Object, hourlyWageInfoValidatorMock.Object, pieceRateWageInfoValidatorMock.Object, workSiteValidatorMock.Object, wioaValidatorMock.Object);
        }

        [TestMethod]
        public void Should_Require_EIN()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EIN, "");
        }

        [TestMethod]
        public void Should_Require_ApplicationTypeId()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ApplicationTypeId, null as int?);
        }

        [TestMethod]
        public void Should_Require_HasPreviousApplication()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.HasPreviousApplication, null as bool?);
        }

        [TestMethod]
        public void Should_Require_EstablishmentType()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EstablishmentType, null as ICollection<ApplicationSubmissionEstablishmentType>);
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EstablishmentType, new List<ApplicationSubmissionEstablishmentType>());
        }

        [TestMethod]
        public void Should_Require_ContactName()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactName, "");
        }

        [TestMethod]
        public void Should_Require_ContactPhone()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactPhone, "");
        }

        [TestMethod]
        public void Should_Require_ContactEmail()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactEmail, "");
        }

        [TestMethod]
        public void Should_Require_PayTypeId()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.PayTypeId, null as int?);
        }

        [TestMethod]
        public void Should_Require_TotalNumWorkSites()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.TotalNumWorkSites, null as int?);
        }

        [TestMethod]
        public void Should_Require_Employer()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.Employer, null as EmployerInfo);
        }

        [TestMethod]
        public void Should_Require_WorkSites()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.WorkSites, null as ICollection<WorkSite>);
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.WorkSites, new List<WorkSite>());
        }

        [TestMethod]
        public void Should_Require_WIOA()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.WIOA, null as WIOA);
        }

        [TestMethod]
        public void Should_Require_CertificateNumber()
        {
            _applicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.CertificateNumber, "");
            var model = new ApplicationSubmission {HasPreviousCertificate = true, CertificateNumber = null};
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.CertificateNumber, model);
        }

        [TestMethod]
        public void Should_Require_HourlyWageInfo()
        {
            var model = new ApplicationSubmission { PayTypeId = 22, HourlyWageInfo = null };
            _applicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.HourlyWageInfo, model);
            model = new ApplicationSubmission {PayTypeId = 21, HourlyWageInfo = null};
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.HourlyWageInfo, model);
        }

        [TestMethod]
        public void Should_Require_PieceRateWageInfo()
        {
            var model = new ApplicationSubmission { PayTypeId = 21, PieceRateWageInfo = null };
            _applicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.PieceRateWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 22, PieceRateWageInfo = null };
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.PieceRateWageInfo, model);
        }

        [TestMethod]
        public void Should_Validate_ContactEmail()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactEmail, "foo123");
        }

        [TestMethod]
        public void Should_Validate_ApplicationType()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ApplicationTypeId, 5);
            _applicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.ApplicationTypeId, 2);
        }

        [TestMethod]
        public void Should_Validate_EstablishmentType()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EstablishmentType, new List<ApplicationSubmissionEstablishmentType> { new ApplicationSubmissionEstablishmentType { EstablishmentTypeId = 9 } });
            _applicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.EstablishmentType, new List<ApplicationSubmissionEstablishmentType> { new ApplicationSubmissionEstablishmentType { EstablishmentTypeId = 5 } });
        }

        [TestMethod]
        public void Should_Validate_PayType()
        {
            _applicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.PayTypeId, 30);
            _applicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.PayTypeId, 22);
        }
    }
}
