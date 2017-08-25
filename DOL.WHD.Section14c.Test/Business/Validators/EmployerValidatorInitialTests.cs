using System;
using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class EmployerValidatorInitialTests
    {
        private static readonly IAddressValidator AddressValidator = new AddressValidator();
        private static readonly IEmployerValidatorInitial EmployerValidator = new EmployerValidatorInitial(AddressValidator);

        [TestMethod]
        public void Should_Require_LegalName()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.LegalName, "");
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.LegalName, "Legal Name");
        }

        [TestMethod]
        public void Should_Require_HasTradeName()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.HasTradeName, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.HasTradeName, false);
        }

        [TestMethod]
        public void Should_Require_LegalNameHasChanged()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.LegalNameHasChanged, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.LegalNameHasChanged, false);
        }

        [TestMethod]
        public void Should_Require_PhysicalAddress()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.PhysicalAddress, null as Address);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.PhysicalAddress, new Address());
        }

        [TestMethod]
        public void Should_Require_HasParentOrg()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.HasParentOrg, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.HasParentOrg, false);
        }

        [TestMethod]
        public void Should_Require_EmployerStatusId()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.EmployerStatusId, null as int?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.EmployerStatusId, ResponseIds.EmployerStatus.PrivateForProfit);
        }

        [TestMethod]
        public void Should_Require_IsEducationalAgency()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.IsEducationalAgency, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.IsEducationalAgency, false);
        }

        [TestMethod]
        public void Should_Not_Require_FiscalQuarterEndDate()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.FiscalQuarterEndDate, null as DateTime?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.FiscalQuarterEndDate, DateTime.Now);
        }

        [TestMethod]
        public void Should_Not_Require_NumSubminimalWageWorkers()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.NumSubminimalWageWorkers, null as WorkerCountInfo);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.NumSubminimalWageWorkers,new WorkerCountInfo());
        }

        [TestMethod]
        public void Should_Require_PCA()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.PCA, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.PCA, false);
        }

        [TestMethod]
        public void Should_Require_SCAId()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.SCAId, null as int?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.SCAId, ResponseIds.SCA.Yes);
        }

        [TestMethod]
        public void Should_Require_EO13658Id()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.EO13658Id, null as int?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.EO13658Id, ResponseIds.EO13658.Yes);
        }

        [TestMethod]
        public void Should_Require_RepresentativePayee()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.RepresentativePayee, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.RepresentativePayee, false);
        }

        [TestMethod]
        public void Should_Require_TakeCreditForCosts()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.TakeCreditForCosts, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.TakeCreditForCosts, false);
        }

        [TestMethod]
        public void Should_Require_ProvidingFacilitiesDeductionType()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionType, null as ICollection<EmployerInfoProvidingFacilitiesDeductionType>);
            var model = new EmployerInfo { TakeCreditForCosts = true, ProvidingFacilitiesDeductionType = null };
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionType, model);
            model = new EmployerInfo
            {
                TakeCreditForCosts = true,
                ProvidingFacilitiesDeductionType =
                    new List<EmployerInfoProvidingFacilitiesDeductionType>
                    {
                        new EmployerInfoProvidingFacilitiesDeductionType {ProvidingFacilitiesDeductionTypeId = ResponseIds.ProvidingFacilitiesDeductionType.Meals}
                    }
            };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionType, model);
        }

        [TestMethod]
        public void Should_Require_TemporaryAuthority()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.TemporaryAuthority, null as bool?);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.TemporaryAuthority, false);
        }

        [TestMethod]
        public void Should_Require_TradeName()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.TradeName, "");
            var model = new EmployerInfo {HasTradeName = true, TradeName = ""};
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.TradeName, model);
            model = new EmployerInfo { HasTradeName = true, TradeName = "Trade Name" };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.TradeName, model);
        }

        [TestMethod]
        public void Should_Require_PriorLegalName()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.PriorLegalName, "");
            var model = new EmployerInfo { LegalNameHasChanged = true, PriorLegalName = "" };
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.PriorLegalName, model);
            model = new EmployerInfo { LegalNameHasChanged = true, PriorLegalName = "Prior Legal Name" };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.PriorLegalName, model);
        }

        [TestMethod]
        public void Should_Require_ParentLegalName()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentLegalName, "");
            var model = new EmployerInfo {HasParentOrg = true, ParentLegalName = ""};
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.ParentLegalName, model);
            model = new EmployerInfo { HasParentOrg = true, ParentLegalName = "Parent Legal Name" };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentLegalName, model);
        }

        [TestMethod]
        public void Should_Require_ParentTradeName()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentTradeName, "");
            var model = new EmployerInfo { HasParentOrg = true, ParentTradeName = "" };
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.ParentTradeName, model);
            model = new EmployerInfo { HasParentOrg = true, ParentTradeName = "Parent Trade Name" };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentTradeName, model);
        }

        [TestMethod]
        public void Should_Require_ParentAddress()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentAddress, null as Address);
            var model = new EmployerInfo { HasParentOrg = true, ParentAddress = null };
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.ParentAddress, model);
            model = new EmployerInfo { HasParentOrg = true, ParentAddress = new Address() };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ParentAddress, model);
        }

        [TestMethod]
        public void Should_Require_SendMailToParent()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.SendMailToParent, null as bool?);
            var model = new EmployerInfo { HasParentOrg = true, SendMailToParent = null };
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.SendMailToParent, model);
            model = new EmployerInfo { HasParentOrg = true, SendMailToParent = false };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.SendMailToParent, model);
        }

        [TestMethod]
        public void Should_Require_EmployerStatusOther()
        {
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.EmployerStatusOther, "");
            var model = new EmployerInfo { EmployerStatusId = ResponseIds.EmployerStatus.Other, EmployerStatusOther = "" };
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.EmployerStatusOther, model);
            model = new EmployerInfo { EmployerStatusId = ResponseIds.EmployerStatus.Other, EmployerStatusOther = "Other" };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.EmployerStatusOther, model);
        }

        [TestMethod]
        public void Should_Validate_EmployerStatus()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.EmployerStatusId, 11);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.EmployerStatusId, ResponseIds.EmployerStatus.PrivateForProfit);
        }

        [TestMethod]
        public void Should_Validate_SCA()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.SCAId, 14);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.SCAId, ResponseIds.SCA.Yes);
        }

        [TestMethod]
        public void Should_Validate_EO13658()
        {
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.EO13658Id, 19);
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.EO13658Id, ResponseIds.EO13658.Yes);
        }

        [TestMethod]
        public void Should_Validate_ProvidingFacilitiesDeductionType()
        {
            var model = new EmployerInfo
            {
                ProvidingFacilitiesDeductionType = new List<EmployerInfoProvidingFacilitiesDeductionType>
                {
                    new EmployerInfoProvidingFacilitiesDeductionType {ProvidingFacilitiesDeductionTypeId = 22}
                },
                TakeCreditForCosts = true
            };
            EmployerValidator.ShouldHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionType, model);

            model = new EmployerInfo
            {
                ProvidingFacilitiesDeductionType = new List<EmployerInfoProvidingFacilitiesDeductionType>
                {
                    new EmployerInfoProvidingFacilitiesDeductionType {ProvidingFacilitiesDeductionTypeId = ResponseIds.ProvidingFacilitiesDeductionType.Meals}
                },
                TakeCreditForCosts = true
            };
            EmployerValidator.ShouldNotHaveValidationErrorFor(x => x.ProvidingFacilitiesDeductionType, model);
        }

        [TestMethod]
        public void Should_Require_SCACount()
        {
            var model = new EmployerInfo {SCAId = ResponseIds.SCA.No, SCACount = null};
            EmployerValidator.ShouldNotHaveValidationErrorFor(e => e.SCACount, model);

            model = new EmployerInfo { SCAId = ResponseIds.SCA.Yes, SCACount = null };
            EmployerValidator.ShouldHaveValidationErrorFor(e => e.SCACount, model);
        }

        [TestMethod]
        public void Should_Require_SCAAttachment()
        {
            var model = new EmployerInfo { SCAId = ResponseIds.SCA.No, SCAAttachmentId = null };
            EmployerValidator.ShouldNotHaveValidationErrorFor(e => e.SCAAttachmentId, model);

            model = new EmployerInfo { SCAId = ResponseIds.SCA.Yes, SCAAttachmentId = null };
            EmployerValidator.ShouldHaveValidationErrorFor(e => e.SCAAttachmentId, model);
        }

        [TestMethod]
        public void Should_Not_Require_TotalDisabledWorkers_No_RepresentativePayee()
        {
            var model = new EmployerInfo {RepresentativePayee = false, TotalDisabledWorkers = null};
            EmployerValidator.ShouldNotHaveValidationErrorFor(e => e.TotalDisabledWorkers, model);
        }

        [TestMethod]
        public void Should_Require_TotalDisabledWorkers_RepresentativePayee()
        {
            var model = new EmployerInfo { RepresentativePayee = true, TotalDisabledWorkers = null };
            EmployerValidator.ShouldHaveValidationErrorFor(e => e.TotalDisabledWorkers, model);
            model = new EmployerInfo { RepresentativePayee = true, TotalDisabledWorkers = 4 };
            EmployerValidator.ShouldNotHaveValidationErrorFor(e => e.TotalDisabledWorkers, model);
        }
    }
}
