using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WorkSiteValidatorTests
    {
        private readonly IWorkSiteValidator _workSiteValidator;

        public WorkSiteValidatorTests()
        {
            var addressValidatorNoCounty = new Mock<IAddressValidatorNoCounty>();
            var employeeValidator = new Mock<IEmployeeValidator>();
            _workSiteValidator = new WorkSiteValidator(addressValidatorNoCounty.Object, employeeValidator.Object);
        }

        [TestMethod]
        public void Should_Require_WorkSiteType()
        {
            _workSiteValidator.ShouldHaveValidationErrorFor(x => x.WorkSiteTypeId, null as int?);
        }

        [TestMethod]
        public void Should_Require_Name()
        {
            _workSiteValidator.ShouldHaveValidationErrorFor(x => x.Name, "");
        }

        [TestMethod]
        public void Should_Require_Address()
        {
            _workSiteValidator.ShouldHaveValidationErrorFor(x => x.Address, null as Address);
        }

        [TestMethod]
        public void Should_Require_SCA()
        {
            _workSiteValidator.ShouldHaveValidationErrorFor(x => x.SCA, null as bool?);
        }

        [TestMethod]
        public void Should_Require_FederalContractWorkPerformed()
        {
            _workSiteValidator.ShouldHaveValidationErrorFor(x => x.FederalContractWorkPerformed, null as bool?);
        }

        [TestMethod]
        public void Should_Require_NumEmployees()
        {
            _workSiteValidator.ShouldHaveValidationErrorFor(x => x.NumEmployees, null as int?);
        }

        [TestMethod]
        public void Should_Require_Employees()
        {
            _workSiteValidator.ShouldHaveValidationErrorFor(x => x.Employees, null as ICollection<Employee>);
        }
    }
}
