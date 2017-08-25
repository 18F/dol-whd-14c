using System;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class ApplicationSubmissionTests
    {
        [TestMethod]
        public void ApplicationSubmission_PublicProperties()
        {
            //Arrange
            var ein = "30-1234567";
            var applicationTypeId = 1;
            var applicationType = new Response {Id = applicationTypeId};
            var hasPreviousApplication = true;
            var hasPreviousCertificate = true;
            var previousCertificateNumber = "xxxxxxx";
            var establishmentTypeId = new List<int> {7, 8, 9};
            var contactName = "Test Name";
            var contactPhone = "123-456-7890";
            var contactFax = "123-456-7890";
            var contactEmail = "foo@bar.com";
            var employer = new EmployerInfo();
            var payTypeId = 21;
            var payType = new Response {Id = payTypeId};
            var hourlyWageInfo = new HourlyWageInfo();
            var pieceRateWageInfo = new PieceRateWageInfo();
            var totalNumWorkSites = 5;
            var workSites = new List<WorkSite>();
            var wioa = new WIOA();
            var statusId = 2;
            var status = new Status {Id = statusId};
            var certificateEffectiveDate = DateTime.Now;
            var certificateExpirationDate = DateTime.Now;
            var certificateNumber = "xxxxxxxxxxxx";

            //Act
            var model = new ApplicationSubmission
            {
                EIN = ein,
                ApplicationTypeId = applicationTypeId,
                ApplicationType = applicationType,
                HasPreviousApplication = hasPreviousApplication,
                HasPreviousCertificate = hasPreviousCertificate,
                PreviousCertificateNumber = previousCertificateNumber,
                EstablishmentTypeId = establishmentTypeId,
                ContactName = contactName,
                ContactPhone = contactPhone,
                ContactFax = contactFax,
                ContactEmail = contactEmail,
                Employer = employer,
                PayTypeId = payTypeId,
                PayType = payType,
                HourlyWageInfo = hourlyWageInfo,
                PieceRateWageInfo = pieceRateWageInfo,
                TotalNumWorkSites = totalNumWorkSites,
                WorkSites = workSites,
                WIOA = wioa,
                Status = status,
                StatusId = statusId,
                CertificateEffectiveDate = certificateEffectiveDate,
                CertificateExpirationDate = certificateExpirationDate,
                CertificateNumber = certificateNumber
            };

            Assert.AreEqual(ein, model.EIN);
            Assert.AreEqual(applicationTypeId, model.ApplicationTypeId);
            Assert.AreEqual(applicationType, model.ApplicationType);
            Assert.AreEqual(hasPreviousApplication, model.HasPreviousApplication);
            Assert.AreEqual(hasPreviousCertificate, model.HasPreviousCertificate);
            Assert.AreEqual(previousCertificateNumber, model.PreviousCertificateNumber);
            Assert.AreEqual(establishmentTypeId[0], model.EstablishmentType.ElementAt(0).EstablishmentTypeId);
            Assert.AreEqual(establishmentTypeId[1], model.EstablishmentType.ElementAt(1).EstablishmentTypeId);
            Assert.AreEqual(establishmentTypeId[2], model.EstablishmentType.ElementAt(2).EstablishmentTypeId);
            Assert.AreEqual(contactName, model.ContactName);
            Assert.AreEqual(contactPhone, model.ContactPhone);
            Assert.AreEqual(contactFax, model.ContactFax);
            Assert.AreEqual(contactEmail, model.ContactEmail);
            Assert.AreEqual(employer, model.Employer);
            Assert.AreEqual(payTypeId, model.PayTypeId);
            Assert.AreEqual(payType, model.PayType);
            Assert.AreEqual(hourlyWageInfo, model.HourlyWageInfo);
            Assert.AreEqual(pieceRateWageInfo, model.PieceRateWageInfo);
            Assert.AreEqual(totalNumWorkSites, model.TotalNumWorkSites);
            Assert.AreEqual(workSites, model.WorkSites);
            Assert.AreEqual(wioa, model.WIOA);
            Assert.AreEqual(statusId, model.StatusId);
            Assert.AreEqual(status, model.Status);
            Assert.AreEqual(certificateEffectiveDate, model.CertificateEffectiveDate);
            Assert.AreEqual(certificateExpirationDate, model.CertificateExpirationDate);
            Assert.AreEqual(certificateNumber, model.CertificateNumber);
        }

        [TestMethod]
        public void ApplicationSubmission_Handles_Null_EstablishmentId()
        {
            // Arrange
            var model = new ApplicationSubmission { EstablishmentTypeId = null};
            Assert.IsNull(model.EstablishmentType);
        }
    }
}
