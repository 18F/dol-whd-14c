using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class AttachmentSupportedFileTypesTest
    {

        [TestMethod]
        public void AttachmentSupportedFileTypes_PublicProperties()
        {
            var id = 5;
            var name = "File Type Name";
            var isActive = true;

            var obj = new AttachmentSupportedFileTypes
            {
                Id = id,
                Name = name,
                IsActive = isActive
            };

            Assert.AreEqual(id, obj.Id);
            Assert.AreEqual(name, obj.Name);
            Assert.AreEqual(isActive, obj.IsActive);
        }
    }
}
