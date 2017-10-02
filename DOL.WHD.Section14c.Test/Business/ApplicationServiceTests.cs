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
                new ApplicationSubmission {Id = appId.ToString()}
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
                new ApplicationSubmission {Id =string.Empty }, //Guid.NewGuid()},
                new ApplicationSubmission {Id = string.Empty }// Guid.NewGuid()}
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
                    SCAWageDeterminationAttachmentId = Guid.NewGuid().ToString()
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNotNull(obj.PieceRateWageInfo.MostRecentPrevailingWageSurvey);
            Assert.IsNull(obj.PieceRateWageInfo.AlternateWageData);
            Assert.IsNull(obj.PieceRateWageInfo.SCAWageDeterminationAttachmentId);
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
                    SCAWageDeterminationAttachmentId = Guid.NewGuid().ToString()
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.PieceRateWageInfo.MostRecentPrevailingWageSurvey);
            Assert.IsNotNull(obj.PieceRateWageInfo.AlternateWageData);
            Assert.IsNull(obj.PieceRateWageInfo.SCAWageDeterminationAttachmentId);
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
                    SCAWageDeterminationAttachmentId = Guid.NewGuid().ToString()
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.PieceRateWageInfo.MostRecentPrevailingWageSurvey);
            Assert.IsNull(obj.PieceRateWageInfo.AlternateWageData);
            Assert.IsNotNull(obj.PieceRateWageInfo.SCAWageDeterminationAttachmentId);
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

        [TestMethod]
        public void ApplicationService_Defaults_SendMailToParent_Null()
        {
            // Arrange
            var obj = new ApplicationSubmission {Employer = new EmployerInfo {HasParentOrg = true}};

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsFalse(obj.Employer.SendMailToParent.Value);
        }

        [TestMethod]
        public void ApplicationService_Defaults_SendMailToParent_NotNull()
        {
            // Arrange
            var obj = new ApplicationSubmission { Employer = new EmployerInfo { HasParentOrg = true, SendMailToParent = true} };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsTrue(obj.Employer.SendMailToParent.Value);
        }

        [TestMethod]
        public void ApplicationService_Cleans_SendMailToParent()
        {
            // Arrange
            var obj = new ApplicationSubmission { Employer = new EmployerInfo { SendMailToParent = true } };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.Employer.SendMailToParent);
        }

        [TestMethod]
        public void ApplicationService_CleansUp_Initial_Application()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                ApplicationTypeId = ResponseIds.ApplicationType.Initial,
                Employer =
                    new EmployerInfo
                    {
                        FiscalQuarterEndDate = DateTime.Now,
                        NumSubminimalWageWorkers = new WorkerCountInfo()
                    },
                PayTypeId = ResponseIds.PayType.Both,
                HourlyWageInfo = new HourlyWageInfo(),
                PieceRateWageInfo = new PieceRateWageInfo(),
                WorkSites = new List<WorkSite>
                {
                    new WorkSite { NumEmployees = 1, Employees = new List<Employee> { new Employee() }}
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNull(obj.Employer.FiscalQuarterEndDate);
            Assert.IsNull(obj.Employer.NumSubminimalWageWorkers);
            Assert.IsNull(obj.PayTypeId);
            Assert.IsNull(obj.HourlyWageInfo);
            Assert.IsNull(obj.PieceRateWageInfo);
            Assert.IsNull(obj.WorkSites.ElementAt(0).NumEmployees);
            Assert.IsNull(obj.WorkSites.ElementAt(0).Employees);
        }

        [TestMethod]
        public void ApplicationService_Does_Not_CleanUp_Renewal_Application()
        {
            // Arrange
            var obj = new ApplicationSubmission
            {
                ApplicationTypeId = ResponseIds.ApplicationType.Renewal,
                Employer =
                    new EmployerInfo
                    {
                        FiscalQuarterEndDate = DateTime.Now,
                        NumSubminimalWageWorkers = new WorkerCountInfo()
                    },
                PayTypeId = ResponseIds.PayType.Both,
                HourlyWageInfo = new HourlyWageInfo(),
                PieceRateWageInfo = new PieceRateWageInfo(),
                WorkSites = new List<WorkSite>
                {
                    new WorkSite { NumEmployees = 1, Employees = new List<Employee> { new Employee() }}
                }
            };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsNotNull(obj.Employer.FiscalQuarterEndDate);
            Assert.IsNotNull(obj.Employer.NumSubminimalWageWorkers);
            Assert.IsNotNull(obj.PayTypeId);
            Assert.IsNotNull(obj.HourlyWageInfo);
            Assert.IsNotNull(obj.PieceRateWageInfo);
            Assert.IsNotNull(obj.WorkSites.ElementAt(0).NumEmployees);
            Assert.IsNotNull(obj.WorkSites.ElementAt(0).Employees);
        }

        [TestMethod]
        public void ApplicationService_Defaults_HasMailingAddress_Null()
        {
            // Arrange
            var obj = new ApplicationSubmission { Employer = new EmployerInfo { HasMailingAddress = null } };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsFalse(obj.Employer.HasMailingAddress.Value);
        }

        [TestMethod]
        public void ApplicationService_Defaults_HasMailingAddress_NotNull()
        {
            // Arrange
            var obj = new ApplicationSubmission { Employer = new EmployerInfo { HasMailingAddress = true } };

            // Act
            _applicationService.ProcessModel(obj);

            // Assert
            Assert.IsTrue(obj.Employer.HasMailingAddress.Value);
        }
    }
}
