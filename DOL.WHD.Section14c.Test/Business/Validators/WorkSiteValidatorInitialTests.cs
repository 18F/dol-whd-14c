using System.Collections.Generic;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class WorkSiteValidatorInitialTests
    {
        private static readonly IAddressValidatorNoCounty AddressValidatorNoCounty = new AddressValidatorNoCounty();
        private static readonly IWorkSiteValidatorInitial WorkSiteValidator = new WorkSiteValidatorInitial(AddressValidatorNoCounty);

        [TestMethod]
        public void Should_Require_WorkSiteType()
        {
            WorkSiteValidator.ShouldHaveValidationErrorFor(x => x.WorkSiteTypeId, null as int?);
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.WorkSiteTypeId, ResponseIds.WorkSiteType.MainEstablishment);
        }

        [TestMethod]
        public void Should_Require_Name()
        {
            WorkSiteValidator.ShouldHaveValidationErrorFor(x => x.Name, "");
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Work Site Name");
        }

        [TestMethod]
        public void Should_Require_Address()
        {
            WorkSiteValidator.ShouldHaveValidationErrorFor(x => x.Address, null as Address);
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.Address, new Address());
        }

        [TestMethod]
        public void Should_Require_SCA()
        {
            WorkSiteValidator.ShouldHaveValidationErrorFor(x => x.SCA, null as bool?);
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.SCA, false);
        }

        [TestMethod]
        public void Should_Require_FederalContractWorkPerformed()
        {
            WorkSiteValidator.ShouldHaveValidationErrorFor(x => x.FederalContractWorkPerformed, null as bool?);
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.FederalContractWorkPerformed, false);
        }

        [TestMethod]
        public void Should_Not_Require_NumEmployees()
        {
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.NumEmployees, null as int?);
        }

        [TestMethod]
        public void Should_Not_Require_Employees()
        {
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.Employees, null as ICollection<Employee>);
        }

        [TestMethod]
        public void Should_Validate_WorkSiteType()
        {
            WorkSiteValidator.ShouldHaveValidationErrorFor(x => x.WorkSiteTypeId, 35);
            WorkSiteValidator.ShouldNotHaveValidationErrorFor(x => x.WorkSiteTypeId, ResponseIds.WorkSiteType.MainEstablishment);
        }
    }
}
