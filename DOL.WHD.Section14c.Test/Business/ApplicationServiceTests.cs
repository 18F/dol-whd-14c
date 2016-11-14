using System;
using DOL.WHD.Section14c.Business;
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
        private readonly ApplicationService _applicationService;
        public ApplicationServiceTests()
        {
            var mockRepo = new Mock<IApplicationRepository>();
            _applicationService = new ApplicationService(mockRepo.Object);
        }

        [TestMethod]
        public void ApplicationService_PublicProperties()
        {
            _applicationService.SubmitApplicationAsync(new ApplicationSubmission());
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
    }
}
