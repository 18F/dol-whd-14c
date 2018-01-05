using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business.Services.Tests
{
    [TestClass()]
    public class EmployerServiceTests
    {
        private IEmployerRepository _employerRepository;
        private Employer _employerToTest;

        [TestInitialize]
        public void Initialize()
        {
            _employerRepository = new EmployerRepositoryMock();
           _employerToTest = new Employer
            {
                EIN = "10-1212111",
                LegalName = "Test Employer",
                PhysicalAddress = new Section14c.Domain.Models.Address()
                {
                    StreetAddress = "123 Main Street",
                    City = "My City",
                    State = "VA",
                    County = "My County",
                    ZipCode = "12345"
                }
            };
        }

        [TestMethod()]
        public void ValidatesEmployerExistTest()
        {
            // Arrange
            var service = new EmployerService(_employerRepository);

            // Act
            var employerObj = service.FindExistingEmployer(_employerToTest);

            // Assert
            Assert.IsNotNull(employerObj);
        }

        [TestMethod()]
        public void ValidatesEmployer_DoesNotExistTest()
        {
            // Arrange
            var service = new EmployerService(_employerRepository);
            _employerToTest.LegalName = "Some Other Name";
            // Act
            var employerObj = service.FindExistingEmployer(_employerToTest);

            // Assert
            Assert.IsNull(employerObj);
        }
    }
}