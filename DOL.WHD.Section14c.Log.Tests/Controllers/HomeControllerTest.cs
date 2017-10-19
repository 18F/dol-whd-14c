using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log;
using DOL.WHD.Section14c.Log.Controllers;

namespace DOL.WHD.Section14c.Log.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
