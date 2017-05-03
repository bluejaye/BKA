using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RefactorMe.Tests
{
    /// <summary>
    /// Make sure the all services still works correct after refactory
    /// </summary>
    [TestClass]
    public class ProductDataConsolidatorTest
    {
        ProductDataConsolidator _test;
        ProductDataConsolidator _test_US;
        ProductDataConsolidator _test_Euro;

        public ProductDataConsolidatorTest()
        {
            _test = new ProductDataConsolidator();
            _test_US = new ProductDataConsolidator();
            _test_Euro = new ProductDataConsolidator();
        }

        [TestMethod]
        public void TestGet()
        {
            var result = _test.Get();
            Assert.IsTrue(result.Count > 0, "Should be more than  result");
        }

        [TestMethod]
        public void GetInUSDollars()
        {
            double rate = 0.76;
            var result = _test.Get();
            var result_US = _test_US.GetInUSDollars();
            Assert.IsTrue(result.Count > 0, "Should be more than result");
            Assert.IsTrue(result_US.Count > 0, "Should be more than result in US");
            Assert.AreEqual(result.Count, result_US.Count, "Should have the same number items");
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Price * rate, result_US[i].Price, "The US price should be apply US rate");
            }
        }

        [TestMethod]
        public void GetInEuros()
        {
            double rate = 0.67;
            var result = _test.Get();
            var result_Euro = _test_Euro.GetInEuros();
            Assert.IsTrue(result.Count > 0, "Should be more than result");
            Assert.IsTrue(result_Euro.Count > 0, "Should be more than result in Euro");
            Assert.AreEqual(result.Count, result_Euro.Count, "Should have the same number items");
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Price * rate, result_Euro[i].Price, "The Euro price should be apply Euro rate");
            }
        }
    }
}
