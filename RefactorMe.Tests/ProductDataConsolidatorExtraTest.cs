using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RefactorMe.Tests
{
    /// <summary>
    /// Test the extention methods works
    /// </summary>
    [TestClass]
    public class ProductDataConsolidatorExtraTest
    {
        ProductDataConsolidatorExtra _test;
        ProductDataConsolidatorExtra _test_US;
        ProductDataConsolidatorExtra _test_Euro;
        ProductDataConsolidatorExtra _test_NZ;  //test the extention method

        public ProductDataConsolidatorExtraTest()
        {
            _test = new ProductDataConsolidatorExtra();
            _test_US = new ProductDataConsolidatorExtra();
            _test_Euro = new ProductDataConsolidatorExtra();
            _test_NZ = new ProductDataConsolidatorExtra();
        }

        [TestMethod]
        public void TestGetInProductDataConsolidatorExtraTest()
        {
            var result = _test.Get();
            Assert.IsTrue(result.Count > 0, "Should be more than  result");
        }

        [TestMethod]
        public void GetInUSDollarsInProductDataConsolidatorExtraTest()
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
        public void GetInEurosInProductDataConsolidatorExtraTest()
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

        /// <summary>
        /// Text extension method
        /// </summary>
        [TestMethod]
        public void GetInNZDollarsInProductDataConsolidatorExtraTest()
        {
            double rate = 0.36;
            var result = _test.Get();
            var result_NZ = _test_NZ.GetInNZDollars();
            Assert.IsTrue(result.Count > 0, "Should be more than result");
            Assert.IsTrue(result_NZ.Count > 0, "Should be more than result in NZ");
            Assert.AreEqual(result.Count, result_NZ.Count, "Should have the same number items");
            bool IsTShirt2Include = false;  //test if new T-Shirt2 type included
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Price * rate, result_NZ[i].Price, "The NZ price should be apply NZ rate");
                if (result[i].Type.Contains("T-Shirt2"))
                {
                    IsTShirt2Include = true;
                }
            }
            Assert.IsTrue(IsTShirt2Include, "New type T-Shirt2 should be included");
        }
    }
}
