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
    public class PhoneCaseTest
    {
        private Services.IService _phoneCaseService;
        Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.PhoneCase>> _phoneCaseRepository = new Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.PhoneCase>>();

        public PhoneCaseTest()
        {
            this.InitialEnvironment();
        }

        private void InitialEnvironment()
        {
            _phoneCaseService = new Services.Implementation.PhoneCaseService(_phoneCaseRepository.Object);

            //Set environment
            DontRefactor.Models.PhoneCase[] list = new PhoneCase[] {
                    new PhoneCase() {
                        Id = Guid.NewGuid(),
                        Name = "Amazon Fire Burgundy Phone Case",
                        Colour = "Burgundy",
                        Material = "PVC",
                        TargetPhone = "Amazon Fire",
                        Price = 14.0
                    },
                    new PhoneCase() {
                        Id = Guid.NewGuid(),
                        Name = "Nokia Lumia 920/930/Icon Crimson Phone Case",
                        Colour = "Red",
                        Material = "Rubber",
                        TargetPhone = "Nokia Lumia 920/930/Icon",
                        Price = 10.0
                    }
                };

            _phoneCaseRepository.Setup(l => l.GetAll()).Returns(list.AsQueryable());
        }

        [TestMethod]
        public void TestList()
        {
            //Act
            Assert.AreEqual(_phoneCaseService.List().Count, 2, "Should bet 2 items");
            Assert.AreEqual(_phoneCaseService.List().First().Type, "Phone Case", "Type should be Phone Case");
        }

        [TestMethod]
        public void TestListInUSDollars()
        {
            //Act
            Assert.AreEqual(_phoneCaseService.ListInUSDollars().Count, 2, "Should bet 2 items");
            Assert.AreEqual(_phoneCaseService.ListInUSDollars().First().Type, "Phone Case", "Type should be Phone Case");
            Assert.AreEqual(_phoneCaseService.ListInUSDollars().First().Price, _phoneCaseService.List().First().Price * 0.76, "US Rate should be applied");
        }

        [TestMethod]
        public void TestListInEuro()
        {
            //Act
            Assert.AreEqual(_phoneCaseService.ListInEuro().Count, 2, "Should bet 2 items");
            Assert.AreEqual(_phoneCaseService.ListInEuro().First().Type, "Phone Case", "Type should be Phone Case");
            Assert.AreEqual(_phoneCaseService.ListInEuro().First().Price, _phoneCaseService.List().First().Price * 0.67, "Euro Rate should be applied");
        }
    }
}
