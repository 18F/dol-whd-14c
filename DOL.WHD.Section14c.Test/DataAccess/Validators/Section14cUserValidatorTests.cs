using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.DataAccess.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.DataAccess.Validators
{
    [TestClass]
    public class Section14cUserValidatorTests
    {
        private readonly Mock<ApplicationUserManager> _applicationUserManagerMock;

        public Section14cUserValidatorTests()
        {
            var userStore = new ApplicationUserStore(new Mock<ApplicationDbContext>().Object);
            _applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore);
        }

        [TestMethod]
        public async Task ValidateUniqueEmail_DuplicateEmail()
        {
            // Arrange
            var emailToTest = "bill.gates@microsoft.com";
            var existingUser = new ApplicationUser { UserName = emailToTest, Email = emailToTest };
            _applicationUserManagerMock.Setup(x => x.FindByEmailAsync(emailToTest)).Returns(Task.FromResult(existingUser));


            var newUser = new ApplicationUser { UserName = "bill.gates@microsoft.com", Email = "bill.gates@microsoft.com" };
            var validator = new Section14cUserValidator<ApplicationUser>(_applicationUserManagerMock.Object) { RequireUniqueEmail = true };

            // Act
            var validatorResult = await validator.ValidateAsync(newUser);

            // Assert
            Assert.IsTrue(validatorResult.Errors.Contains($"Email '{emailToTest}' is already taken."));
        }

        [TestMethod]
        public async Task ValidateUniqueEmail_UniqueEmail()
        {
            // Arrange
            var existingEmailToTest = "bill.gates@microsoft.com";
            var newEmailToTest = "paul.allen@microsoft.com";
            var existingUser = new ApplicationUser { UserName = existingEmailToTest, Email = existingEmailToTest };
            _applicationUserManagerMock.Setup(x => x.FindByEmailAsync(existingEmailToTest)).Returns(Task.FromResult(existingUser));


            var newUser = new ApplicationUser { UserName = newEmailToTest, Email = newEmailToTest };
            var validator = new Section14cUserValidator<ApplicationUser>(_applicationUserManagerMock.Object) { RequireUniqueEmail = true };

            // Act
            var validatorResult = await validator.ValidateAsync(newUser);

            // Assert
            Assert.IsTrue(validatorResult.Succeeded);
        }

        [TestMethod]
        public async Task ValidateUniqueEIN_DuplicateEIN()
        {
            // Arrange
            var existingEmailToTest = "bill.gates@microsoft.com";
            var newEmailToTest = "paul.allen@microsoft.com";
            var einToTest = "12-3456789";
            var existingUser = new ApplicationUser { UserName = existingEmailToTest, Email = existingEmailToTest };
            existingUser.Organizations.Add(new OrganizationMembership { EIN = einToTest, IsPointOfContact = true });
            _applicationUserManagerMock.Setup(x => x.Users).Returns(new List<ApplicationUser> { existingUser }.AsQueryable());


            var newUser = new ApplicationUser { UserName = newEmailToTest, Email = newEmailToTest };
            newUser.Organizations.Add(new OrganizationMembership { EIN = einToTest, IsPointOfContact = true });
            var validator = new Section14cUserValidator<ApplicationUser>(_applicationUserManagerMock.Object) { RequireUniqueEINAdmin = true };

            // Act
            var validatorResult = await validator.ValidateAsync(newUser);

            // Assert
            Assert.IsTrue(validatorResult.Errors.Contains("EIN is already registered"));
        }

        [TestMethod]
        public async Task ValidateUniqueEIN_UniqueEIN()
        {
            // Arrange
            var existingEmailToTest = "bill.gates@microsoft.com";
            var newEmailToTest = "paul.allen@microsoft.com";
            var existingEINToTest = "12-3456789";
            var newEINToTest = "98-7654321";
            var existingUser = new ApplicationUser { UserName = existingEmailToTest, Email = existingEmailToTest };
            existingUser.Organizations.Add(new OrganizationMembership { EIN = existingEINToTest, IsPointOfContact = true });
            _applicationUserManagerMock.Setup(x => x.Users).Returns(new List<ApplicationUser> { existingUser }.AsQueryable());


            var newUser = new ApplicationUser { UserName = newEmailToTest, Email = newEmailToTest };
            newUser.Organizations.Add(new OrganizationMembership { EIN = newEINToTest, IsPointOfContact = true });
            var validator = new Section14cUserValidator<ApplicationUser>(_applicationUserManagerMock.Object) { RequireUniqueEINAdmin = true };

            // Act
            var validatorResult = await validator.ValidateAsync(newUser);

            // Assert
            Assert.IsTrue(validatorResult.Succeeded);
        }

        [TestMethod]
        public async Task DoesntValidateUniqueEINOnPasswordChange()
        {
            // Arrange
            var ein = "12-3456789";
            var existingUser = new ApplicationUser { UserName = "steve.jobs@apple.com", Email = "steve.jobs@apple.com" };
            existingUser.Organizations.Add(new OrganizationMembership { EIN = ein, IsPointOfContact = true });
            _applicationUserManagerMock.Setup(x => x.Users).Returns(new List<ApplicationUser> { existingUser }.AsQueryable());
            var validator = new Section14cUserValidator<ApplicationUser>(_applicationUserManagerMock.Object) { RequireUniqueEINAdmin = true };

            // Act (running the validator on a user already in the store simulates what happens when a user changes their password)
            var result = await validator.ValidateAsync(existingUser);

            // Assert
            Assert.IsTrue(result.Succeeded);
        }
    }
}
