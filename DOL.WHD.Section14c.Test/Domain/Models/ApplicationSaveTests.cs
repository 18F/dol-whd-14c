using System;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            // Act
            var obj = new ApplicationSave
            {
                EIN = ein,
                ApplicationState = state
            };

            // Assert
            Assert.AreEqual(ein, obj.EIN);
            Assert.AreEqual(state, obj.ApplicationState);
        }
    }
}
