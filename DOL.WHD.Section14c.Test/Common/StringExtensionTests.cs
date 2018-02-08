using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Common.Extensions;

namespace DOL.WHD.Section14c.Test.Common
{
    [TestClass()]
    public class StringExtensionTests
    {
        [TestMethod()]
        public void TrimAndToLowerCaseTest()
        {
            string strToTest = " This is a  Test ";
            var result = strToTest.TrimAndToLowerCase();
            Assert.AreEqual("this is a test", result);
        }

        [TestMethod()]
        public void TrimAndToLowerCaseTestEmptyString()
        {
            string strToTest = "";
            var result = strToTest.TrimAndToLowerCase();
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod()]
        public void TrimAndConvertWhitespacesToSingleSpaces()
        {
            string strToTest = " This is a    Test    ";
            var result = strToTest.TrimAndConvertWhitespacesToSingleSpaces();
            Assert.AreEqual("This is a Test", result);
        }

        [TestMethod()]
        public void TrimAndConvertWhitespacesToSingleSpacesTestEmptyString()
        {
            string strToTest = "";
            var result = strToTest.TrimAndConvertWhitespacesToSingleSpaces();
            Assert.AreEqual(string.Empty, result);
        }
    }
}