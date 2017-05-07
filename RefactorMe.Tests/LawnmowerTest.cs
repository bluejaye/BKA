using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace RefactorMe.Tests
{
    /// <summary>
    /// Make sure the all services still works correct after refactory
    /// </summary>
    [TestClass]
    public class LawnmowerTest
    {
        private Services.IService _lawnmowerService;
        Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.Lawnmower>> _lawnmowerRepository = new Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.Lawnmower>>();

        public LawnmowerTest()
        {
            this.InitialEnvironment();
        }

        private void InitialEnvironment()
        {
            _lawnmowerService = new Services.Implementation.LawnmowerService(_lawnmowerRepository.Object);

            //Set environment
            DontRefactor.Models.Lawnmower[] list = new DontRefactor.Models.Lawnmower[] {
                    new DontRefactor.Models.Lawnmower() {
                        Id = Guid.NewGuid(),
                        Name = "Hewlett-Packard Rideable Lawnmower",
                        FuelEfficiency = "Very Low",
                        IsVehicle = true,
                        Price = 3000.0
                    },
                    new DontRefactor.Models.Lawnmower() {
                        Id = Guid.NewGuid(),
                        Name = "Fisher Price's My First Lawnmower",
                        FuelEfficiency = "Ultimate",
                        IsVehicle = false,
                        Price = 45.0
                    },
                    new DontRefactor.Models.Lawnmower() {
                        Id = Guid.NewGuid(),
                        Name = "Volkswagen LawnMaster 39000B Lawnmower",
                        FuelEfficiency = "Moderate",
                        IsVehicle = false,
                        Price = 1020.0
                    }
                };

            _lawnmowerRepository.Setup(l => l.GetAll()).Returns(list.AsQueryable());
        }

        [TestMethod]
        public void TestList()
        {
            //Act
            Assert.AreEqual(_lawnmowerService.List().Count, 3, "Should bet 3 items");
            Assert.AreEqual(_lawnmowerService.List().First().Type, "Lawnmower", "Type should be Lawnmower");
        }

        [TestMethod]
        public void TestListInUSDollars()
        {
            //Act
            Assert.AreEqual(_lawnmowerService.ListInUSDollars().Count, 3, "Should bet 3 items");
            Assert.AreEqual(_lawnmowerService.ListInUSDollars().First().Type, "Lawnmower", "Type should be Lawnmower");
            Assert.AreEqual(_lawnmowerService.ListInUSDollars().First().Price, _lawnmowerService.List().First().Price * 0.76, "US Rate should be applied");
        }

        [TestMethod]
        public void TestListInEuro()
        {
            //Act
            Assert.AreEqual(_lawnmowerService.ListInEuro().Count, 3, "Should bet 3 items");
            Assert.AreEqual(_lawnmowerService.ListInEuro().First().Type, "Lawnmower", "Type should be Lawnmower");
            Assert.AreEqual(_lawnmowerService.ListInEuro().First().Price, _lawnmowerService.List().First().Price * 0.67, "Euro Rate should be applied");
        }
    }
}
