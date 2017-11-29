using System;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class PrevailingWageSurveyInfoTests
    {
        [TestMethod]
        public void PrevailingWageSurveyInfo_PublicProperties()
        {
            var prevailingWageDetermined = 50.50;
            var sourceEmployers = new List<SourceEmployer>();
            var attachmentId = Guid.NewGuid().ToString();
            var attachment = new Attachment {Id = attachmentId.ToString() };

            var model = new PrevailingWageSurveyInfo
            {
                PrevailingWageDetermined = prevailingWageDetermined,
                SourceEmployers = sourceEmployers,
                AttachmentId = attachmentId,
                Attachment = attachment
            };

            Assert.AreEqual(prevailingWageDetermined, model.PrevailingWageDetermined);
            Assert.AreEqual(sourceEmployers, model.SourceEmployers);
            Assert.AreEqual(attachmentId, model.AttachmentId);
            Assert.AreEqual(attachment, model.Attachment);
        }
    }
}
