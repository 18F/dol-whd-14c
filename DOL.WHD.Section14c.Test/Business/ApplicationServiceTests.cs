using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ApplicationServiceTests
    {
        private readonly Mock<IApplicationRepository> _mockRepo;
        private readonly ApplicationService _applicationService;
        public ApplicationServiceTests()
        {
            _mockRepo = new Mock<IApplicationRepository>();
            _applicationService = new ApplicationService(_mockRepo.Object);
        }

        [TestMethod]
        public void ApplicationService_PublicProperties()
        {
            _applicationService.SubmitApplicationAsync(new ApplicationSubmission());
        }

        [TestMethod]
        public void ApplicationService_ReturnsApplication()
        {
            // Arrange
            var appId = Guid.NewGuid();
            var applications = new List<ApplicationSubmission>
            {
                new ApplicationSubmission {Id = appId}
            };
            _mockRepo.Setup(x => x.Get()).Returns(applications.AsQueryable());

            // Act
            var application = _applicationService.GetApplicationById(appId);

            // Assert
            Assert.AreEqual(applications[0], application);
        }

        [TestMethod]
        public void ApplicationService_ReturnsAllApplications()
        {
            // Arrange
            var applications = new List<ApplicationSubmission>
            {
                new ApplicationSubmission {Id = Guid.NewGuid()},
                new ApplicationSubmission {Id = Guid.NewGuid()}
            };
            _mockRepo.Setup(x => x.Get()).Returns(applications.AsQueryable());

            // Act
            var obj = _applicationService.GetAllApplications();

            // Assert
            Assert.AreEqual(2, obj.Count());
        }

        [TestMethod]
        public async Task ApplicationService_Changes_ApplicationStatus()
        {
            // Arrange
            var oldStatusId = 1;
            var newStatusId = 2;
            var application = new ApplicationSubmission {StatusId = oldStatusId};
            
            // Act
            await _applicationService.ChangeApplicationStatus(application, newStatusId);

            // Assert
            Assert.AreEqual(application.StatusId, newStatusId);
            _mockRepo.Verify(m => m.ModifyApplication(It.IsAny<ApplicationSubmission>()));

        }

        [TestMethod]
        public void ApplicationService_CleanupHourlyWageInfo()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                PayTypeId = ResponseIds.PayType.PieceRate,
                HourlyWageInfo = new HourlyWageInfo()
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.HourlyWageInfo);
        }

        [TestMethod]
        public void ApplicationService_CleanupPieceRateWageInfo()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                PayTypeId = ResponseIds.PayType.Hourly,
                PieceRateWageInfo = new PieceRateWageInfo()
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.PieceRateWageInfo);
        }

        [TestMethod]
        public void ApplicationService_Cleanup_PrevailingWageSurvey()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                PieceRateWageInfo = new PieceRateWageInfo
                {
                    PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey,
                    MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo(),
                    AlternateWageData = new AlternateWageData(),
                    SCAWageDeterminationId = Guid.NewGuid()
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNotNull(obj.PieceRateWageInfo.MostRecentPrevailingWageSurvey);
            Assert.IsNull(obj.PieceRateWageInfo.AlternateWageData);
            Assert.IsNull(obj.PieceRateWageInfo.SCAWageDeterminationId);
        }

        [TestMethod]
        public void ApplicationService_Cleanup_AlternateWageData()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                PieceRateWageInfo = new PieceRateWageInfo
                {
                    PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.AlternateWageData,
                    MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo(),
                    AlternateWageData = new AlternateWageData(),
                    SCAWageDeterminationId = Guid.NewGuid()
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.PieceRateWageInfo.MostRecentPrevailingWageSurvey);
            Assert.IsNotNull(obj.PieceRateWageInfo.AlternateWageData);
            Assert.IsNull(obj.PieceRateWageInfo.SCAWageDeterminationId);
        }

        [TestMethod]
        public void ApplicationService_Cleanup_SCAWageDetermination()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                PieceRateWageInfo = new PieceRateWageInfo
                {
                    PrevailingWageMethodId = ResponseIds.PrevailingWageMethod.SCAWageDetermination,
                    MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo(),
                    AlternateWageData = new AlternateWageData(),
                    SCAWageDeterminationId = Guid.NewGuid()
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.PieceRateWageInfo.MostRecentPrevailingWageSurvey);
            Assert.IsNull(obj.PieceRateWageInfo.AlternateWageData);
            Assert.IsNotNull(obj.PieceRateWageInfo.SCAWageDeterminationId);
        }

        [TestMethod]
        public void ApplicationService_Sets_PendingStatus()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                Status = new Status(),
                StatusId = StatusIds.Issued // make sure any set status gets overwritten with pending
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.Status);
            Assert.AreEqual(StatusIds.Pending, obj.StatusId);
        }

        [TestMethod]
        public void ApplicationService_Defaults_AdminFields()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                CertificateEffectiveDate = DateTime.Now,
                CertificateExpirationDate = DateTime.Now,
                CertificateNumber = "xxxxxxxx"
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.CertificateEffectiveDate);
            Assert.IsNull(obj.CertificateExpirationDate);
            Assert.IsNull(obj.CertificateNumber);
        }
    }
}
