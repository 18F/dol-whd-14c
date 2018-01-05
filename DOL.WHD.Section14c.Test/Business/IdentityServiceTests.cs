using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DOL.WHD.Section14c.Domain.ViewModels;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class IdentityServiceTests
    {
        private readonly Mock<IPrincipal> _mockUser;
        private readonly Mock<ClaimsIdentity> _mockIdentity;

        public IdentityServiceTests()
        {
            _mockUser = new Mock<IPrincipal>();
            _mockIdentity = new Mock<ClaimsIdentity>();
        }

        [TestMethod]
        public void ValidatesEINClaims_HasClaim()
        {
            // Arrange
            var einToTest = "30-1234567";
            var claims = new List<Claim>
            {
                new Claim("Id", einToTest)
            };
            _mockIdentity.Setup(i => i.Claims).Returns(claims);
            _mockUser.Setup(u => u.Identity).Returns(_mockIdentity.Object);
            var service = new IdentityService();

            // Act
            var hasClaim = service.UserHasEINClaim(_mockUser.Object, einToTest);

            // Assert
            Assert.IsTrue(hasClaim);
        }

        [TestMethod]
        public void ValidatesEINClaims_DoesNotHaveClaim()
        {
            // Arrange
            var einToTest = "30-1234567";
            var einToCheck = "30-9876543";
            var claims = new List<Claim>
            {
                new Claim("Id", einToTest)
            };
            _mockIdentity.Setup(i => i.Claims).Returns(claims);
            _mockUser.Setup(u => u.Identity).Returns(_mockIdentity.Object);
            var service = new IdentityService();

            // Act
            var hasClaim = service.UserHasEINClaim(_mockUser.Object, einToCheck);

            // Assert
            Assert.IsFalse(hasClaim);
        }


        [TestMethod]
        public void ValidatesFeatureClaims_HasClaim()
        {
            // Arrange
            var featureToTest = ApplicationClaimTypes.ViewAllApplications;
            var claims = new List<Claim>
            {
                new Claim(featureToTest, true.ToString())
            };
            _mockIdentity.Setup(i => i.Claims).Returns(claims);
            _mockUser.Setup(u => u.Identity).Returns(_mockIdentity.Object);
            var service = new IdentityService();

            // Act
            var hasClaim = service.UserHasFeatureClaim(_mockUser.Object, featureToTest);

            // Assert
            Assert.IsTrue(hasClaim);
        }

        [TestMethod]
        public void ValidatesFeatureClaims_DoesNotHaveClaim()
        {
            // Arrange
            var featureToTest = ApplicationClaimTypes.SubmitApplication;
            var featureToCheck = ApplicationClaimTypes.ViewAllApplications;
            var claims = new List<Claim>
            {
                new Claim(featureToTest, true.ToString())
            };
            _mockIdentity.Setup(i => i.Claims).Returns(claims);
            _mockUser.Setup(u => u.Identity).Returns(_mockIdentity.Object);
            var service = new IdentityService();

            // Act
            var hasClaim = service.UserHasFeatureClaim(_mockUser.Object, featureToCheck);

            // Assert
            Assert.IsFalse(hasClaim);
        }

        [TestMethod]
        public void ValidatesSystemAdminCanCreateNewApplication()
        {
            // Arrange
            var employerIdToCheck = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32";
            var obj = new UserInfoViewModel
            {
                Roles = new List<RoleViewModel>() { new RoleViewModel() { Name = Roles.SystemAdministrator } }
            };
            
            var service = new IdentityService();

            // Act
            var hasPermission = service.HasAddPermission(obj, employerIdToCheck);

            // Assert
            Assert.IsTrue(hasPermission);
        }

        [TestMethod]
        public void ValidatesUserCanCreateNewApplication()
        {
            // Arrange
            var employerIdToCheck = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32";

            var obj = new UserInfoViewModel
            {
                Roles = new List<RoleViewModel>() { new RoleViewModel() { Name = Roles.Applicant } },
                Organizations = new List<OrganizationMembership>()
                        { new OrganizationMembership()
                            { Employer = new Section14c.Domain.Models.Submission.Employer(){ Id = employerIdToCheck } }
                        }
            };

            var service = new IdentityService();

            // Act
            var hasPermission = service.HasAddPermission(obj, employerIdToCheck);

            // Assert
            Assert.IsTrue(hasPermission);
        }

        [TestMethod]
        public void ValidatesUser_CanNotCreateNewApplication()
        {
            // Arrange
            var employerIdToTest = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e66";
            var employerIdToCheck = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32";

            var obj = new UserInfoViewModel
            {
                Roles = new List<RoleViewModel>() { new RoleViewModel() { Name = Roles.Applicant } },
                Organizations = new List<OrganizationMembership>()
                        { new OrganizationMembership()
                            { Employer = new Section14c.Domain.Models.Submission.Employer(){ Id = employerIdToTest } }
                        }
            };

            var service = new IdentityService();

            // Act
            var hasPermission = service.HasAddPermission(obj, employerIdToCheck);

            // Assert
            Assert.IsFalse(hasPermission);
        }

        [TestMethod]
        public void ValidatesSystemAdminCanSaveApplication()
        {
            // Arrange
            var employerIdToCheck = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32";
            var obj = new UserInfoViewModel
            {
                Roles = new List<RoleViewModel>() { new RoleViewModel() { Name = Roles.SystemAdministrator } }
            };

            var service = new IdentityService();

            // Act
            var hasPermission = service.HasSavePermission(obj, employerIdToCheck);

            // Assert
            Assert.IsTrue(hasPermission);
        }

        [TestMethod]
        public void ValidatesUserCanSaveApplication()
        {
            // Arrange
            var employerIdToCheck = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32";

            var obj = new UserInfoViewModel
            {
                Roles = new List<RoleViewModel>() { new RoleViewModel() { Name = Roles.Applicant } },
                Organizations = new List<OrganizationMembership>()
                        { new OrganizationMembership() { ApplicationId = employerIdToCheck } }
            };

            var service = new IdentityService();

            // Act
            var hasPermission = service.HasSavePermission(obj, employerIdToCheck);

            // Assert
            Assert.IsTrue(hasPermission);
        }

        [TestMethod]
        public void ValidatesUser_CanNotSaveApplication()
        {
            // Arrange
            var employerIdToTest = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e66";
            var employerIdToCheck = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32";

            var obj = new UserInfoViewModel
            {
                Roles = new List<RoleViewModel>() { new RoleViewModel() { Name = Roles.Applicant } },
                Organizations = new List<OrganizationMembership>()
                         { new OrganizationMembership() { ApplicationId = employerIdToTest } }
            };

            var service = new IdentityService();

            // Act
            var hasPermission = service.HasSavePermission(obj, employerIdToCheck);

            // Assert
            Assert.IsFalse(hasPermission);
        }
    }
}
