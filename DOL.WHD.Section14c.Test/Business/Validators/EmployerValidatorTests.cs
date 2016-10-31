using System;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class EmployerValidatorTests
    {
        private readonly IEmployerValidator _employerValidator;

        public EmployerValidatorTests()
        {
            var addressValidator = new Mock<IAddressValidator>();
            var workerCountInfoValidator = new Mock<IWorkerCountInfoValidator>();
            _employerValidator = new EmployerValidator(addressValidator.Object, workerCountInfoValidator.Object);
        }

        [TestMethod]
        public void Should_Require_LegalName()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.LegalName, "");
        }

        [TestMethod]
        public void Should_Require_HasTradeName()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.HasTradeName, null as bool?);
        }

        [TestMethod]
        public void Should_Require_LegalNameHasChanged()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.LegalNameHasChanged, null as bool?);
        }

        [TestMethod]
        public void Should_Require_PhysicalAddress()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.PhysicalAddress, null as Address);
        }

        [TestMethod]
        public void Should_Require_HasDifferentMailingAddress()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.HasDifferentMailingAddress, null as bool?);
        }

        [TestMethod]
        public void Should_Require_HasParentOrg()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.HasParentOrg, null as bool?);
        }

        [TestMethod]
        public void Should_Require_EmployerStatusId()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.EmployerStatusId, null as int?);
        }

        [TestMethod]
        public void Should_Require_IsEducationalAgency()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.IsEducationalAgency, null as bool?);
        }

        [TestMethod]
        public void Should_Require_FiscalQuarterEndDate()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.FiscalQuarterEndDate, default(DateTime));
        }

        [TestMethod]
        public void Should_Require_NumSubminimalWageWorkers()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.NumSubminimalWageWorkers, null as WorkerCountInfo);
        }

        [TestMethod]
        public void Should_Require_PCA()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.PCA, null as bool?);
        }

        [TestMethod]
        public void Should_Require_SCAId()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.SCAId, null as int?);
        }

        [TestMethod]
        public void Should_Require_EO13658Id()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.EO13658Id, null as int?);
        }

        [TestMethod]
        public void Should_Require_RepresentativePayee()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.RepresentativePayee, null as bool?);
        }

        [TestMethod]
        public void Should_Require_TakeCreditForCosts()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.TakeCreditForCosts, null as bool?);
        }

        [TestMethod]
        public void Should_Require_ProvidingFacilitiesDeductionTypeId()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionTypeId, null as int?);
        }

        [TestMethod]
        public void Should_Require_TemporaryAuthority()
        {
            _employerValidator.ShouldHaveValidationErrorFor(x => x.TemporaryAuthority, null as bool?);
        }

        [TestMethod]
        public void Should_Require_TradeName()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.TradeName, "");
            var model = new EmployerInfo {HasTradeName = true, TradeName = ""};
            _employerValidator.ShouldHaveValidationErrorFor(x => x.TradeName, model);
        }

        [TestMethod]
        public void Should_Require_PriorLegalName()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.PriorLegalName, "");
            var model = new EmployerInfo { LegalNameHasChanged = true, PriorLegalName = "" };
            _employerValidator.ShouldHaveValidationErrorFor(x => x.PriorLegalName, model);
        }

        [TestMethod]
        public void Should_Require_ParentLegalName()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentLegalName, "");
            var model = new EmployerInfo {HasParentOrg = true, ParentLegalName = ""};
            _employerValidator.ShouldHaveValidationErrorFor(x => x.ParentLegalName, model);
        }

        [TestMethod]
        public void Should_Require_ParentTradeName()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentTradeName, "");
            var model = new EmployerInfo { HasParentOrg = true, ParentTradeName = "" };
            _employerValidator.ShouldHaveValidationErrorFor(x => x.ParentTradeName, model);
        }

        [TestMethod]
        public void Should_Require_ParentAddress()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentAddress, null as Address);
            var model = new EmployerInfo { HasParentOrg = true, ParentAddress = null };
            _employerValidator.ShouldHaveValidationErrorFor(x => x.ParentLegalName, model);
        }

        [TestMethod]
        public void Should_Require_SendMailToParent()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.SendMailToParent, null as bool?);
            var model = new EmployerInfo { HasParentOrg = true, SendMailToParent = null };
            _employerValidator.ShouldHaveValidationErrorFor(x => x.SendMailToParent, model);
        }

        [TestMethod]
        public void Should_Require_EmployerStatusOther()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.EmployerStatusOther, "");
            var model = new EmployerInfo { EmployerStatusId = 10, EmployerStatusOther = "" };
            _employerValidator.ShouldHaveValidationErrorFor(x => x.EmployerStatusOther, model);
        }

        [TestMethod]
        public void Should_Require_ProvidingFacilitiesDeductionTypeOther()
        {
            _employerValidator.ShouldNotHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionTypeOther, "");
            var model = new EmployerInfo { ProvidingFacilitiesDeductionTypeId = 20, ProvidingFacilitiesDeductionTypeOther = "" };
            _employerValidator.ShouldHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionTypeOther, model);
        }
    }
}
