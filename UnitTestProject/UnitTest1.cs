using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDaD.Toolkit.Database;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Row r = new Row(1);
            Assert.IsTrue(1 == r.Nr);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Row r = new Row(1);
            Assert.IsTrue(2 == r.Nr);
        }
    }
}
