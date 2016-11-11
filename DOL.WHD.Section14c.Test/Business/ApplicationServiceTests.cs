using System;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
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
            var applications = new List<ApplicationSubmission>()
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
        public void ApplicationService_CleanupHourlyWageInfo()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                PayTypeId = 22,
                HourlyWageInfo = new HourlyWageInfo()
            };

            // Act
            _applicationService.CleanupModel(obj);

            // Assert
            Assert.IsNull(obj.HourlyWageInfo);
        }

        [TestMethod]
        public void ApplicationService_CleanupPieceRateWageInfo()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                PayTypeId = 21,
                PieceRateWageInfo = new PieceRateWageInfo()
            };

            // Act
            _applicationService.CleanupModel(obj);

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
                    PrevailingWageMethodId = 24,
                    MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo(),
                    AlternateWageData = new AlternateWageData(),
                    SCAWageDeterminationId = Guid.NewGuid()
                }
            };

            // Act
            _applicationService.CleanupModel(obj);

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
                    PrevailingWageMethodId = 25,
                    MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo(),
                    AlternateWageData = new AlternateWageData(),
                    SCAWageDeterminationId = Guid.NewGuid()
                }
            };

            // Act
            _applicationService.CleanupModel(obj);

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
                    PrevailingWageMethodId = 26,
                    MostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo(),
                    AlternateWageData = new AlternateWageData(),
                    SCAWageDeterminationId = Guid.NewGuid()
                }
            };

            // Act
            _applicationService.CleanupModel(obj);

            // Assert
            Assert.IsNull(obj.PieceRateWageInfo.MostRecentPrevailingWageSurvey);
            Assert.IsNull(obj.PieceRateWageInfo.AlternateWageData);
            Assert.IsNotNull(obj.PieceRateWageInfo.SCAWageDeterminationId);
        }
    }
}
