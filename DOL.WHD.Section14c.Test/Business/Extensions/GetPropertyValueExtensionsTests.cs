using DOL.WHD.Section14c.Business.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.PdfApi.Business.Extensions.Tests
{
    [TestClass()]
    public class GetPropertyValueExtensionsTests
    {
        public class InnerObjectType
        {
            public string Child { get; set; }
            public int Number { get; set; }
        }

        private Object TestObject;

        [TestInitialize]
        public void Initialize()
        {
            TestObject = new
            {
                StringProp = "this is a string",
                ObjectProp = new InnerObjectType()
                {
                    Child = "this is a grandchild node",
                    Number = 10
                }
            };
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetPropValue_NullObject()
        {
            Object value = GetPropertyValueExtensions.GetPropValue(null, "");
            Assert.IsNull(value);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetPropertyValueExtensions_GetPropValue_NoProperty()
        {
            Object value = GetPropertyValueExtensions.GetPropValue(TestObject, null);
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetPropValue_NoExistantProperty()
        {
            Object value = GetPropertyValueExtensions.GetPropValue(TestObject, "fake-property");
            Assert.IsNull(value);
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetPropValue_FirstChild()
        {
            Object value = GetPropertyValueExtensions.GetPropValue(TestObject, "StringProp");
            Assert.AreEqual("this is a string", value);
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetPropValue_LaterChild()
        {
            Object value = GetPropertyValueExtensions.GetPropValue(TestObject, "ObjectProp.Child");
            Assert.AreEqual("this is a grandchild node", value);
        }

        public void GetPropertyValueExtensions_GetPropValueGeneric_NullObject()
        {
            Object value = GetPropertyValueExtensions.GetPropValue<Object>(null, "");
            Assert.IsNull(value);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetPropertyValueExtensions_GetPropValueGeneric_NoProperty()
        {
            Object value = GetPropertyValueExtensions.GetPropValue<Object>(TestObject, null);
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetPropValueGeneric_NoExistantProperty()
        {
            Object value = GetPropertyValueExtensions.GetPropValue<Object>(TestObject, "fake-property");
            Assert.IsNull(value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetPropertyValueExtensions_GetPropValueGeneric_InvalidType()
        {
            Object value = GetPropertyValueExtensions.GetPropValue<List<bool>>(TestObject, "StringProp");
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetPropValueGeneric_ValidType()
        {
            int value = GetPropertyValueExtensions.GetPropValue<int>(TestObject, "ObjectProp.Number");
            Assert.AreEqual(10, value);
        }

        public void GetPropertyValueExtensions_GetStringPropValue_NullObject()
        {
            String value = GetPropertyValueExtensions.GetStringPropValue(null, "");
            Assert.IsNull(value);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetPropertyValueExtensions_GetStringPropValue_NoProperty()
        {
            String value = GetPropertyValueExtensions.GetStringPropValue(TestObject, null);
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetStringPropValue_NoExistantProperty()
        {
            String value = GetPropertyValueExtensions.GetStringPropValue(TestObject, "fake-property");
            Assert.IsNull(value);
        }

        [TestMethod]
        public void GetPropertyValueExtensions_GetStringPropValue_ValidType()
        {
            String value = GetPropertyValueExtensions.GetStringPropValue(TestObject, "StringProp");
            Assert.AreEqual("this is a string", value);
        }
    }
}