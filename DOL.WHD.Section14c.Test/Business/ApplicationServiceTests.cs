using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ApplicationServiceTests
    {
        [TestMethod]
        public void ApplicationService_PublicProperties()
        {
            var mockRepo = new Mock<IApplicationRepository>();
            var obj = new ApplicationService(mockRepo.Object);
            obj.SubmitApplicationAsync(new ApplicationSubmission());
        }
    }
}
