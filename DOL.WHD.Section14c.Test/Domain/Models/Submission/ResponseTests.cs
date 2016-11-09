using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class ResponseTests
    {
        [TestMethod]
        public void Response_PublicProperties()
        {
            // Arrange
            var id = 1;
            var questionKey = "EmployerStatus";
            var display = "Private, For Profit";
            var subDisplay = "Some explanatory text";
            var otherValueKey = "Other Value Key";
            var isActive = true;

            // Act
            var model = new Response
            {
                Id = id,
                QuestionKey = questionKey,
                Display = display,
                SubDisplay = subDisplay,
                OtherValueKey = otherValueKey,
                IsActive = isActive
            };

            // Assert
            Assert.AreEqual(id, model.Id);
            Assert.AreEqual(questionKey, model.QuestionKey);
            Assert.AreEqual(display, model.Display);
            Assert.AreEqual(subDisplay, model.SubDisplay);
            Assert.AreEqual(otherValueKey, model.OtherValueKey);
            Assert.AreEqual(isActive, model.IsActive);
        }
    }
}
