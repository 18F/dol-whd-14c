using System;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace DOL.WHD.Section14c.Test.Domain.ViewModels
{
    [TestClass]
    public class ApplicationSaveDTOTests
    {
        [TestMethod]
        public void ApplicationSaveDTO_PublicProperties()
        {
            // Arrange
            var ein = "30-1234567";
            var applicationId = Guid.NewGuid();
            var state = JObject.Parse(@"
                {
                    ""userId"" : ""5"",
                    ""email"" : ""foo@bar.com""
                }
            ");
            

            // Act
            var obj = new ApplicationSaveDTO()
            {
                EIN = ein,
                ApplicationId = applicationId,
                State = state
            };

            // Assert
            Assert.AreEqual(ein, obj.EIN);
            Assert.AreEqual(applicationId, obj.ApplicationId);
            Assert.AreEqual(state, obj.State);
        }
    }
}
