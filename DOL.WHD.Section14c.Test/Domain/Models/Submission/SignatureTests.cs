using System;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class SignatureTests
    {
        [TestMethod]
        public void Signature_PublicProperties()
        {
            var agreement = true;
            var firstName = "First Name";
            var lastName = "Last Name";
            var title = "Title";
            var date = DateTime.Now;

            var model = new Signature
            {
                Agreement = agreement,
                FirstName = firstName,
                LastName = lastName,
                Title = title,
                Date = date
            };

            Assert.AreEqual(agreement, model.Agreement);
            Assert.AreEqual(firstName, model.FirstName);
            Assert.AreEqual(lastName, model.LastName);
            Assert.AreEqual(title, model.Title);
            Assert.AreEqual(date, model.Date);
        }
    }
}
