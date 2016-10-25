using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
                new Claim("EIN", einToTest)
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
                new Claim("EIN", einToTest)
            };
            _mockIdentity.Setup(i => i.Claims).Returns(claims);
            _mockUser.Setup(u => u.Identity).Returns(_mockIdentity.Object);
            var service = new IdentityService();

            // Act
            var hasClaim = service.UserHasEINClaim(_mockUser.Object, einToCheck);

            // Assert
            Assert.IsFalse(hasClaim);
        }
    }
}
