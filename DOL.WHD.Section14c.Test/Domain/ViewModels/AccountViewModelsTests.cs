using System;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class AccountViewModelsTests
    {
        [TestMethod]
        public void ChangePasswordBindingModel_PublicProperties()
        {
            var obj = new UserInfoViewModel
            {
                Email = "email",
                UserId = "userid",
                Organizations = new List<OrganizationMembership>() {Capacity = 1}
            };

            Assert.AreEqual("email", obj.Email);
            Assert.AreEqual("userid", obj.UserId);
            Assert.IsTrue((new List<OrganizationMembership>()).SequenceEqual(obj.Organizations));
        }
    }
}
