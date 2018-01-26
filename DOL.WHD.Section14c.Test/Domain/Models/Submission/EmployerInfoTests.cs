using System;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class EmployerInfoTests
    {
        [TestMethod]
        public void EmployerInfo_PublicProperties()
        {
            var id = Guid.NewGuid().ToString();
            var legalName = "Employer Legal Name";
            var hasTradeName = true;
            var tradeName = "Employer Trade Name";
            var legalNameHasChanged = true;
            var priorLegalName = "Prior Legal Name";
            var physicalAddress = new Address();
            var hasParentOrg = true;
            var parentLegalName = "Parent Legal Name";
            var parentTradeName = "Parent Trade Name";
            var parentAddress = new Address();
            var sendMailToParent = true;
            var employerStatusId = 25;
            var employerStatus = new Response {Id = employerStatusId};
            var employerStatusOther = "Employer Status Other";
            var isEducationalAgency = true;
            var fiscalQuarterEndDate = DateTime.Now;
            var numSubminimalWageWorkers = new WorkerCountInfo();
            var pca = true;
            var scaId = 41;
            var sca = new Response {Id = scaId};
            var scaCount = 5;
            var scaAttachmentId = Guid.NewGuid().ToString();
            var scaAttaachment = new Attachment {Id = scaAttachmentId };
            var eo13658Id = 50;
            var eo13658 = new Response {Id = eo13658Id};
            var representativePayee = true;
            var takeCreditForCosts = true;
            var providingFacilitiesDeductionTypeId = new List<int> {15, 16, 17};
            var temporaryAuthority = true;

            var model = new EmployerInfo
            {
                Id= id,
                LegalName = legalName,
                HasTradeName = hasTradeName,
                TradeName = tradeName,
                LegalNameHasChanged = legalNameHasChanged,
                PriorLegalName = priorLegalName,
                PhysicalAddress = physicalAddress,
                HasParentOrg = hasParentOrg,
                ParentLegalName = parentLegalName,
                ParentTradeName = parentTradeName,
                ParentAddress = parentAddress,
                SendMailToParent = sendMailToParent,
                EmployerStatusId = employerStatusId,
                EmployerStatus = employerStatus,
                EmployerStatusOther = employerStatusOther,
                IsEducationalAgency = isEducationalAgency,
                FiscalQuarterEndDate = fiscalQuarterEndDate,
                NumSubminimalWageWorkers = numSubminimalWageWorkers,
                PCA = pca,
                SCAId = scaId,
                SCA = sca,
                SCACount = scaCount,
                SCAAttachments = new List<EmployerInfoSCAAttachment>() { new EmployerInfoSCAAttachment { EmployerInfoId = id, SCAAttachment = scaAttaachment, AttachmentName ="test.pdf" } },
                EO13658Id = eo13658Id,
                EO13658 = eo13658,
                RepresentativePayee = representativePayee,
                TakeCreditForCosts = takeCreditForCosts,
                ProvidingFacilitiesDeductionTypeId = providingFacilitiesDeductionTypeId,
                TemporaryAuthority = temporaryAuthority
            };

            Assert.AreEqual(legalName, model.LegalName);
            Assert.AreEqual(hasTradeName, model.HasTradeName);
            Assert.AreEqual(tradeName, model.TradeName);
            Assert.AreEqual(legalNameHasChanged, model.LegalNameHasChanged);
            Assert.AreEqual(priorLegalName, model.PriorLegalName);
            Assert.AreEqual(physicalAddress, model.PhysicalAddress);
            Assert.AreEqual(hasParentOrg, model.HasParentOrg);
            Assert.AreEqual(parentLegalName, model.ParentLegalName);
            Assert.AreEqual(parentTradeName, model.ParentTradeName);
            Assert.AreEqual(parentAddress, model.ParentAddress);
            Assert.AreEqual(sendMailToParent, model.SendMailToParent);
            Assert.AreEqual(employerStatusId, model.EmployerStatusId);
            Assert.AreEqual(employerStatus, model.EmployerStatus);
            Assert.AreEqual(employerStatusOther, model.EmployerStatusOther);
            Assert.AreEqual(isEducationalAgency, model.IsEducationalAgency);
            Assert.AreEqual(fiscalQuarterEndDate, model.FiscalQuarterEndDate);
            Assert.AreEqual(numSubminimalWageWorkers, model.NumSubminimalWageWorkers);
            Assert.AreEqual(pca, model.PCA);
            Assert.AreEqual(scaId, model.SCAId);
            Assert.AreEqual(sca, model.SCA);
            Assert.AreEqual(scaCount, model.SCACount);
            Assert.AreEqual(scaAttaachment, model.SCAAttachments.FirstOrDefault().SCAAttachment);
            Assert.AreEqual(eo13658Id, model.EO13658Id);
            Assert.AreEqual(eo13658, model.EO13658);
            Assert.AreEqual(representativePayee, model.RepresentativePayee);
            Assert.AreEqual(takeCreditForCosts, model.TakeCreditForCosts);
            Assert.AreEqual(providingFacilitiesDeductionTypeId[0], model.ProvidingFacilitiesDeductionType.ElementAt(0).ProvidingFacilitiesDeductionTypeId);
            Assert.AreEqual(providingFacilitiesDeductionTypeId[1], model.ProvidingFacilitiesDeductionType.ElementAt(1).ProvidingFacilitiesDeductionTypeId);
            Assert.AreEqual(providingFacilitiesDeductionTypeId[2], model.ProvidingFacilitiesDeductionType.ElementAt(2).ProvidingFacilitiesDeductionTypeId);
            Assert.AreEqual(temporaryAuthority, model.TemporaryAuthority);
        }

        [TestMethod]
        public void EmployerInfo_Handles_Null_ProvidingFacilitiesDeductionTypeId()
        {
            // Arrange
            var model = new EmployerInfo { ProvidingFacilitiesDeductionTypeId = null };
            Assert.IsNull(model.ProvidingFacilitiesDeductionType);
        }
    }
}
