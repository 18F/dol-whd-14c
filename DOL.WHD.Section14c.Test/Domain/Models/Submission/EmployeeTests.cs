using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models.Submission
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void Employee_PublicProperties()
        {
            var name = "Employee Name";
            var primaryDisabilityId = 39;
            var primaryDisability = new Response {Id = primaryDisabilityId};
            var primaryDisabilityOther = "primaryDisabilityOther";
            var workType = "workType";
            var numJobs = 3;
            var avgWeeklyHours = 41.5;
            var avgHourlyEarnings = 3.55;
            var prevailingWage = 5.05;
            var productivityMeasure = 500.55;
            var commensurateWageRate = "commensurateWageRate";
            var totalHours = 500118.55;
            var workAtOtherSite = true;

            var model = new Employee
            {
                Name = name,
                PrimaryDisabilityId = primaryDisabilityId,
                PrimaryDisability = primaryDisability,
                PrimaryDisabilityOther = primaryDisabilityOther,
                WorkType = workType,
                NumJobs = numJobs,
                AvgWeeklyHours = avgWeeklyHours,
                AvgHourlyEarnings = avgHourlyEarnings,
                PrevailingWage = prevailingWage,
                ProductivityMeasure = productivityMeasure,
                CommensurateWageRate = commensurateWageRate,
                TotalHours = totalHours,
                WorkAtOtherSite = workAtOtherSite
            };

            Assert.AreEqual(name, model.Name);
            Assert.AreEqual(primaryDisabilityId, model.PrimaryDisabilityId);
            Assert.AreEqual(primaryDisability, model.PrimaryDisability);
            Assert.AreEqual(primaryDisabilityOther, model.PrimaryDisabilityOther);
            Assert.AreEqual(workType, model.WorkType);
            Assert.AreEqual(numJobs, model.NumJobs);
            Assert.AreEqual(avgWeeklyHours, model.AvgWeeklyHours);
            Assert.AreEqual(avgHourlyEarnings, model.AvgHourlyEarnings);
            Assert.AreEqual(prevailingWage, model.PrevailingWage);
            Assert.AreEqual(productivityMeasure, model.ProductivityMeasure);
            Assert.AreEqual(commensurateWageRate, model.CommensurateWageRate);
            Assert.AreEqual(totalHours, model.TotalHours);
            Assert.AreEqual(workAtOtherSite, model.WorkAtOtherSite);
        }
    }
}
