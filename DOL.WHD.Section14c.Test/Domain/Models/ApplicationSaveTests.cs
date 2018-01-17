using System;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Test.Domain.Models
{
    [TestClass]
    public class ApplicationSaveTests
    {
        [TestMethod]
        public void ApplicationSave_PublicProperties()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var ein = "30-1234567";
            var state = @"
                {
                    ""userId"" : ""5"",
                    ""email"" : ""foo@bar.com""
                }
            ";
            var employer = new Employer() { Id = "123" };

            // Act
            var obj = new ApplicationSave
            {
                Id = ein,
                ApplicationState = state,
                Employer = employer,
                Employer_Id = "123"
            };

            // Assert
            Assert.AreEqual(ein, obj.Id);
            Assert.AreEqual(state, obj.ApplicationState);
            Assert.AreEqual(employer.Id, obj.Employer_Id);

        }
    }
}
