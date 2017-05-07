using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using RefactorMe.DontRefactor.Models;

namespace RefactorMe.Tests
{
    /// <summary>
    /// Make sure the all services still works correct after refactory
    /// </summary>
    [TestClass]
    public class TShirtTest
    {
        private Services.IService _tShirtService;
        Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.TShirt>> _tShirtRepository = new Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.TShirt>>();

        public TShirtTest()
        {
            this.InitialEnvironment();
        }

        private void InitialEnvironment()
        {
            _tShirtService = new Services.Implementation.TShirtService(_tShirtRepository.Object);

            //Set environment
            DontRefactor.Models.TShirt[] list = new TShirt[] {
                    new TShirt() {
                        Id = Guid.NewGuid(),
                        Colour = "Blue",
                        Name = "Xamarin C# T-Shirt",
                        Price = 15.0,
                        ShirtText = "C#, Xamarin"
                    },
                    new TShirt() {
                        Id = Guid.NewGuid(),
                        Colour = "Black",
                        Name = "New York Yankees T-Shirt",
                        Price = 8.0,
                        ShirtText = "NY"
                    },
                    new TShirt() {
                        Id = Guid.NewGuid(),
                        Colour = "Green",
                        Name = "Disney Sleeping Beauty T-Shirt",
                        Price = 10.0,
                        ShirtText = "Mirror mirror on the wall..."
                    }
                };

            _tShirtRepository.Setup(l => l.GetAll()).Returns(list.AsQueryable());
        }

        [TestMethod]
        public void TestList()
        {
            //Act
            Assert.AreEqual(_tShirtService.List().Count, 3, "Should bet 3 items");
            Assert.AreEqual(_tShirtService.List().First().Type, "TShirt", "Type should be TShirt");
        }

        [TestMethod]
        public void TestListInUSDollars()
        {
            //Act
            Assert.AreEqual(_tShirtService.ListInUSDollars().Count, 3, "Should bet 3 items");
            Assert.AreEqual(_tShirtService.ListInUSDollars().First().Type, "TShirt", "Type should be TShirt");
            Assert.AreEqual(_tShirtService.ListInUSDollars().First().Price, _tShirtService.List().First().Price * 0.76, "US Rate should be applied");
        }

        [TestMethod]
        public void TestListInEuro()
        {
            //Act
            Assert.AreEqual(_tShirtService.ListInEuro().Count, 3, "Should bet 3 items");
            Assert.AreEqual(_tShirtService.ListInEuro().First().Type, "TShirt", "Type should be TShirt");
            Assert.AreEqual(_tShirtService.ListInEuro().First().Price, _tShirtService.List().First().Price * 0.67, "Euro Rate should be applied");
        }
    }
}
