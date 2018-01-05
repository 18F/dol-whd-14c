using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class OrganizationServiceTests
    {
        private IOrganizationRepository _organizationRepository;
        private Employer _employerToTest;

        [TestInitialize]
        public void Initialize()
        {
            _organizationRepository = new OrganizationRepositoryMock();
            _employerToTest = new Employer() { Id = "123456" };
        }

        [TestMethod()]
        public void ValidatesOrganizationExistTest()
        {
            // Arrange
            var service = new OrganizationService(_organizationRepository);

            // Act
            var organizationMembershipObj = service.GetOrganizationMembershipByEmployer(_employerToTest);

            // Assert
            Assert.AreEqual(organizationMembershipObj.ApplicationId, "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32");
        }

        [TestMethod()]
        public void ValidatesOrganization_DoesNotExistTest()
        {
            // Arrange
            var service = new OrganizationService(_organizationRepository);
            _employerToTest.Id = "1231231231231";
            // Act
            var organizationMembershipObj = service.GetOrganizationMembershipByEmployer(_employerToTest);

            // Assert
            Assert.IsNull(organizationMembershipObj);
        }
    }
}
