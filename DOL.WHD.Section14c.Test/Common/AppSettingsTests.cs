using System.Configuration;
using DOL.WHD.Section14c.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Common
{
    [TestClass]
    public class AppSettingsTests
    {
        [TestMethod]
        [ExpectedException(typeof(SettingsPropertyNotFoundException))]

        public void SetttingNotAvailable()
        {
            AppSettings.Get<bool>("unknown");
        }

        [TestMethod]

        public void SetttingConfigured_ReturnsCorrectBool()
        {
            var results = AppSettings.Get<bool>("BoolConfig");
            Assert.AreEqual(results, true);
        }

        [TestMethod]

        public void SetttingConfigured_ReturnsInt()
        {
            var results = AppSettings.Get<int>("IntConfig");
            Assert.AreEqual(results, 1);
        }

        [TestMethod]

        public void SetttingConfigured_ReturnsString()
        {
            var results = AppSettings.Get<string>("StringConfig");
            Assert.AreEqual(results, "value");
        }

        [TestMethod]

        public void SetttingConfigured_ReturnsDouble()
        {
            var results = AppSettings.Get<double>("DoubleConfig");
            Assert.AreEqual(results, 123.4);
        }

    }
}
