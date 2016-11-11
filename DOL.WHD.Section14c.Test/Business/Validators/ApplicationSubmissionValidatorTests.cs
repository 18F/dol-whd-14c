using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class ApplicationSubmissionValidatorTests
    {
        // dependencies
        private static readonly IAddressValidatorNoCounty AddressValidatorNoCounty = new AddressValidatorNoCounty();
        private static readonly IAddressValidator AddressValidator = new AddressValidator();
        private static readonly IWorkerCountInfoValidator WorkerCountInfoValidator = new WorkerCountInfoValidator();
        private static readonly IEmployerValidator EmployerValidator = new EmployerValidator(AddressValidator, WorkerCountInfoValidator);
        private static readonly ISourceEmployerValidator SourceEmployerValidator = new SourceEmployerValidator(AddressValidator);
        private static readonly IPrevailingWageSurveyInfoValidator PrevailingWageSurveyInfoValidator = new PrevailingWageSurveyInfoValidator(SourceEmployerValidator);
        private static readonly IAlternateWageDataValidator AlternateWageDataValidator = new AlternateWageDataValidator();
        private static readonly IHourlyWageInfoValidator HourlyWageInfoValidator = new HourlyWageInfoValidator(PrevailingWageSurveyInfoValidator, AlternateWageDataValidator);
        private static readonly IPieceRateWageInfoValidator PieceRateWageInfoValidator = new PieceRateWageInfoValidator(PrevailingWageSurveyInfoValidator, AlternateWageDataValidator);
        private static readonly IEmployeeValidator EmployeeValidator = new EmployeeValidator();
        private static readonly IWorkSiteValidator WorkSiteValidator = new WorkSiteValidator(AddressValidatorNoCounty, EmployeeValidator);
        private static readonly IWIOAWorkerValidator WIOAWorkerValidator = new WIOAWorkerValidator();
        private static readonly IWIOAValidator WIOAValidator = new WIOAValidator(WIOAWorkerValidator);

        private static readonly ApplicationSubmissionValidator ApplicationSubmissionValidator = new ApplicationSubmissionValidator(EmployerValidator, HourlyWageInfoValidator, PieceRateWageInfoValidator, WorkSiteValidator, WIOAValidator);

        [TestMethod]
        public void Should_Require_EIN()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EIN, "");
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.EIN, "30-123457");
        }

        [TestMethod]
        public void Should_Require_ApplicationTypeId()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ApplicationTypeId, null as int?);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.ApplicationTypeId, 2);
        }

        [TestMethod]
        public void Should_Require_HasPreviousApplication()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.HasPreviousApplication, null as bool?);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.HasPreviousApplication, false);
        }

        [TestMethod]
        public void Should_Require_EstablishmentType()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EstablishmentType, null as ICollection<ApplicationSubmissionEstablishmentType>);
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EstablishmentType, new List<ApplicationSubmissionEstablishmentType>());
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.EstablishmentType, new List<ApplicationSubmissionEstablishmentType> { new ApplicationSubmissionEstablishmentType { EstablishmentTypeId = 5} });
        }

        [TestMethod]
        public void Should_Require_ContactName()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactName, "");
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.ContactName, "Contact Name");
        }

        [TestMethod]
        public void Should_Require_ContactPhone()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactPhone, "");
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.ContactPhone, "123-456-7890");
        }

        [TestMethod]
        public void Should_Not_Require_ContactFax()
        {
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.ContactFax, "");
        }

        [TestMethod]
        public void Should_Require_ContactEmail()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactEmail, "");
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.ContactEmail, "foo@bar.com");
        }

        [TestMethod]
        public void Should_Require_PayTypeId()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.PayTypeId, null as int?);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.PayTypeId, 22);
        }

        [TestMethod]
        public void Should_Require_TotalNumWorkSites()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.TotalNumWorkSites, null as int?);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.TotalNumWorkSites, 5);
        }

        [TestMethod]
        public void Should_Require_Employer()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.Employer, null as EmployerInfo);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.Employer, new EmployerInfo());
        }

        [TestMethod]
        public void Should_Require_WorkSites()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.WorkSites, null as ICollection<WorkSite>);
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.WorkSites, new List<WorkSite>());
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.WorkSites, new List<WorkSite> { new WorkSite() });
        }

        [TestMethod]
        public void Should_Require_WIOA()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.WIOA, null as WIOA);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.WIOA, new WIOA());
        }

        [TestMethod]
        public void Should_Require_CertificateNumber()
        {
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.CertificateNumber, "");
            var model = new ApplicationSubmission {HasPreviousCertificate = true, CertificateNumber = null};
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.CertificateNumber, model);
            model = new ApplicationSubmission { HasPreviousCertificate = true, CertificateNumber = "12345" };
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.CertificateNumber, model);
        }

        [TestMethod]
        public void Should_Require_HourlyWageInfo()
        {
            var model = new ApplicationSubmission { PayTypeId = 22, HourlyWageInfo = null };
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.HourlyWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 21, HourlyWageInfo = new HourlyWageInfo() };
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.HourlyWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 23, HourlyWageInfo = new HourlyWageInfo() };
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.HourlyWageInfo, model);
            model = new ApplicationSubmission {PayTypeId = 21, HourlyWageInfo = null};
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.HourlyWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 23, HourlyWageInfo = null };
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.HourlyWageInfo, model);
        }

        [TestMethod]
        public void Should_Require_PieceRateWageInfo()
        {
            var model = new ApplicationSubmission { PayTypeId = 21, PieceRateWageInfo = null };
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.PieceRateWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 22, PieceRateWageInfo = new PieceRateWageInfo() };
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.PieceRateWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 23, PieceRateWageInfo = new PieceRateWageInfo() };
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.PieceRateWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 22, PieceRateWageInfo = null };
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.PieceRateWageInfo, model);
            model = new ApplicationSubmission { PayTypeId = 23, PieceRateWageInfo = null };
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.PieceRateWageInfo, model);
        }

        [TestMethod]
        public void Should_Validate_ContactEmail()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ContactEmail, "foo123");
        }

        [TestMethod]
        public void Should_Validate_ApplicationType()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.ApplicationTypeId, 5);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.ApplicationTypeId, 2);
        }

        [TestMethod]
        public void Should_Validate_EstablishmentType()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.EstablishmentType, new List<ApplicationSubmissionEstablishmentType> { new ApplicationSubmissionEstablishmentType { EstablishmentTypeId = 9 } });
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.EstablishmentType, new List<ApplicationSubmissionEstablishmentType> { new ApplicationSubmissionEstablishmentType { EstablishmentTypeId = 5 } });
        }

        [TestMethod]
        public void Should_Validate_PayType()
        {
            ApplicationSubmissionValidator.ShouldHaveValidationErrorFor(x => x.PayTypeId, 30);
            ApplicationSubmissionValidator.ShouldNotHaveValidationErrorFor(x => x.PayTypeId, 22);
        }
    }
}
