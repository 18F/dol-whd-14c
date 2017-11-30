using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Domain.Models.Identity
{
    [TestClass]
    public class ApplicationUserTests
    {
        [TestMethod]
        public async Task GenerateUserIdentityAsync_AddsEINClaim()
        {
            // Arrange
            var ein = "30-1234567";
            var identity = new ClaimsIdentity();
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object);
            userManagerMock.Setup(x => x.CreateIdentityAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(identity);

            var roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
            var roleManagerMock = new Mock<RoleManager<ApplicationRole>>(roleStoreMock.Object);

            var user = new ApplicationUser();
            user.Organizations = new List<OrganizationMembership>
            {
                new OrganizationMembership { EIN = ein, IsPointOfContact = true}
            };

            // Act
            var result = await user.GenerateUserIdentityAsync(userManagerMock.Object, roleManagerMock.Object, null);

            Assert.AreEqual(ein, result.Claims.Single(x => x.Type == "EIN").Value);
        }

        [TestMethod]
        public async Task GenerateUserIdentityAsync_AddsSubmitApplicationClaim()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object);
            userManagerMock.Setup(x => x.CreateIdentityAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(identity);

            var roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
            var roleManagerMock = new Mock<RoleManager<ApplicationRole>>(roleStoreMock.Object);

            var user = new ApplicationUser();

            // Act
            var result = await user.GenerateUserIdentityAsync(userManagerMock.Object, roleManagerMock.Object, null);

            Assert.IsTrue(result.HasClaim(x => x.Type == ApplicationClaimTypes.SubmitApplication));
        }

        [TestMethod]
        public async Task GenerateUserIdentityAsync_AddsRoleBasedClaim()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object);
            userManagerMock.Setup(x => x.CreateIdentityAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(identity);

            var roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = Roles.SystemAdministrator,
                    RoleFeatures = new List<RoleFeature>
                    {
                        new RoleFeature { Feature = new Feature { Key = ApplicationClaimTypes.ModifyAccount}}
                    }
                }
            };
            var roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
            var roleManagerMock = new Mock<RoleManager<ApplicationRole>>(roleStoreMock.Object);
            roleManagerMock.Setup(x => x.Roles).Returns(roles.AsQueryable());

            var user = new ApplicationUser();
            user.Roles.Add(new ApplicationUserRole { RoleId = Roles.SystemAdministrator});

            // Act
            var result = await user.GenerateUserIdentityAsync(userManagerMock.Object, roleManagerMock.Object, null);

            Assert.IsTrue(result.HasClaim(x => x.Type == ApplicationClaimTypes.ModifyAccount));
        }
    }
}
