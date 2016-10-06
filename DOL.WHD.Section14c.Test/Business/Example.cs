using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.Business.Test.RepositoryMocks;
using DOL.WHD.Section14c.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DOL.WHD.Section14c.Business.Test.Business
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

        [TestMethod]
        public void DisposeTest()
        {
            var repo = new ExampleRepositoryMock();
            var example = new ExampleService(repo);
            example.Dispose();
            Assert.IsTrue(repo.Disposed);
        }

    }
}
