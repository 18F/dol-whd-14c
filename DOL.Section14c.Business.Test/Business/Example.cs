using System;
using System.Collections.Generic;
using System.Linq;
using DOL.Section14c.Business.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.Section14c.Business.Test.Business
{
    [TestClass]
    public class Example
    {
        [TestMethod]
        public void NubmersAddUp()
        {
            var example = new Services.ExampleService(new ExampleRepositoryMock());
            Assert.AreEqual(example.AddNumbers(new List<int>() { 1, 1 }), 2);
        }
        [TestMethod]
        public void CanRetrieveNumbersInOrder()
        {
            var example = new Services.ExampleService(new ExampleRepositoryMock());
            CollectionAssert.AreEqual(example.GetNumbers().ToList(), new List<int>() { 1, 2, 3 });
        }
    }
}
