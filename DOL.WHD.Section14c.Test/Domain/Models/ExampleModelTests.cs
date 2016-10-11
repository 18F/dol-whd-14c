using System;
using DOL.WHD.Section14c.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Domain.Models
{
    [TestClass]
    public class ExampleModelTests
    {
        [TestMethod]
        public void ExampleModel_PublicProperties()
        {
            var obj = new ExampleModel
            {
                Number = 1
            };

            Assert.AreEqual(1, obj.Number);
        }
    }
}
