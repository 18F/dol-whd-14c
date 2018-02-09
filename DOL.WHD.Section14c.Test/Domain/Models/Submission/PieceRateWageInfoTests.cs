using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class PieceRateWageInfoTests
    {
        [TestMethod]
        public void PieceRateWageInfo_PublicProperties()
        {
            var pieceRateWorkDescription = "pieceRateWorkDescription";
            var prevailingWageDeterminedForJob = 1.5;
            var standardProductivity = 7.5;
            var pieceRatePaidToWorkers = 14.55;
            var numWorkers = 5;
            var jobName = "Job Name";
            var jobDescription = "Job Description";
            var prevailingWageMethodId = 35;
            var prevailingWageMethod = new Response { Id = prevailingWageMethodId };
            var mostRecentPrevailingWageSurvey = new PrevailingWageSurveyInfo();
            var alternateWageData = new AlternateWageData();
            var scaWageDeterminationId = Guid.NewGuid().ToString();
            var scaWageDetermination = new Attachment { Id = scaWageDeterminationId.ToString() };
            var attachmentId = Guid.NewGuid().ToString();
            var attachment = new Attachment { Id = attachmentId.ToString() };

            var model = new PieceRateWageInfo
            {
                PieceRateWorkDescription = pieceRateWorkDescription,
                PrevailingWageDeterminedForJob = prevailingWageDeterminedForJob,
                StandardProductivity = standardProductivity,
                PieceRatePaidToWorkers = pieceRatePaidToWorkers,
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

            Assert.AreEqual(pieceRateWorkDescription, model.PieceRateWorkDescription);
            Assert.AreEqual(prevailingWageDeterminedForJob, model.PrevailingWageDeterminedForJob);
            Assert.AreEqual(standardProductivity, model.StandardProductivity);
            Assert.AreEqual(pieceRatePaidToWorkers, model.PieceRatePaidToWorkers);
            Assert.AreEqual(numWorkers, model.NumWorkers);
            Assert.AreEqual(jobName, model.JobName);
            Assert.AreEqual(jobDescription, model.JobDescription);
            Assert.AreEqual(prevailingWageMethodId, model.PrevailingWageMethodId);
            Assert.AreEqual(prevailingWageMethod, model.PrevailingWageMethod);
            Assert.AreEqual(mostRecentPrevailingWageSurvey, model.MostRecentPrevailingWageSurvey);
            Assert.AreEqual(alternateWageData, model.AlternateWageData);
            Assert.AreEqual(scaWageDetermination, model.SCAWageDeterminationAttachment);
            Assert.AreEqual(attachmentId, model.AttachmentId);
            Assert.AreEqual(attachment, model.Attachment);
        }
    }
}
