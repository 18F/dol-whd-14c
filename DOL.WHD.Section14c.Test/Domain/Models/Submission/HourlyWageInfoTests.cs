using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class HourlyWageInfoTests
    {
        [TestMethod]
        public void HourlyWageInfo_PublicProperties()
        {
            var id = Guid.NewGuid().ToString();
            var workMeasurementFrequency = "workMeasurementFrequency";
            var numWorkers = 5;
            var jobName = "Job Name";
            var jobDescription = "Job Description";
            var prevailingWageMethodId = 35;
            var prevailingWageMethod = new Response {Id = prevailingWageMethodId};
            var mostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo();
            var alternateWageData = new AlternateWageData();
            var scaWageDeterminationId = Guid.NewGuid().ToString();
            var scaWageDetermination =  new Attachment { Id = scaWageDeterminationId.ToString() };
            var attachmentId = Guid.NewGuid().ToString();
            var attachment = new Attachment {Id = attachmentId.ToString() };

            var model = new HourlyWageInfo
            {
                Id=id,
                WorkMeasurementFrequency = workMeasurementFrequency,
                NumWorkers = numWorkers,
                JobName = jobName,
                JobDescription = jobDescription,
                PrevailingWageMethodId = prevailingWageMethodId,
                PrevailingWageMethod = prevailingWageMethod,
                MostRecentPrevailingWageSurvey = mostRecentPrevailingWageSurvey,
                AlternateWageData = alternateWageData,
                SCAWageDeterminationAttachment = scaWageDetermination,
                AttachmentId = attachmentId,
                Attachment = attachment
            };

            Assert.AreEqual(workMeasurementFrequency, model.WorkMeasurementFrequency);
            Assert.AreEqual(numWorkers, model.NumWorkers);
            Assert.AreEqual(jobName, model.JobName);
            Assert.AreEqual(jobDescription, model.JobDescription);
            Assert.AreEqual(prevailingWageMethodId, model.PrevailingWageMethodId);
            Assert.AreEqual(prevailingWageMethod, model.PrevailingWageMethod);
            Assert.AreEqual(mostRecentPrevailingWageSurvey, model.MostRecentPrevailingWageSurvey);
            Assert.AreEqual(alternateWageData, model.AlternateWageData);
            Assert.AreEqual(attachmentId, model.AttachmentId);
            Assert.AreEqual(attachment, model.Attachment);
        }
    }
}
