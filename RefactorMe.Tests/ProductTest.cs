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
    public class ProductTest
    {
        private Services.Implementation.BaseService<Lawnmower> _lawnmowerService;
        Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.Lawnmower>> _lawnmowerRepository = new Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.Lawnmower>>();
        private Services.Implementation.BaseService<PhoneCase> _phoneCaseService;
        Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.PhoneCase>> _phoneCaseRepository = new Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.PhoneCase>>();
        private Services.Implementation.BaseService<TShirt> _tShirtService;
        Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.TShirt>> _tShirtRepository = new Mock<DontRefactor.Data.IReadOnlyRepository<DontRefactor.Models.TShirt>>();
        private Services.IService _productService;

        public ProductTest()
        {
            this.InitialEnvironment();
        }

        private void InitialEnvironment()
        {
            _lawnmowerService = new Services.Implementation.LawnmowerService(_lawnmowerRepository.Object);

            //Set environment
            DontRefactor.Models.Lawnmower[] lawnmowerList = new DontRefactor.Models.Lawnmower[] {
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

            _lawnmowerRepository.Setup(l => l.GetAll()).Returns(lawnmowerList.AsQueryable());

            _phoneCaseService = new Services.Implementation.PhoneCaseService(_phoneCaseRepository.Object);

            //Set environment
            DontRefactor.Models.PhoneCase[] phoneCaseList = new PhoneCase[] {
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

            _phoneCaseRepository.Setup(l => l.GetAll()).Returns(phoneCaseList.AsQueryable());

            _tShirtService = new Services.Implementation.TShirtService(_tShirtRepository.Object);

            //Set environment
            DontRefactor.Models.TShirt[] tShirtList = new TShirt[] {
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

            _tShirtRepository.Setup(l => l.GetAll()).Returns(tShirtList.AsQueryable());

            _productService = new Services.Implementation.ProductService(_lawnmowerService, _phoneCaseService, _tShirtService);
        }
        
        [TestMethod]
        public void TestList()
        {
            //Act
            Assert.AreEqual(_productService.List().Count, 8, "Should bet 8 items");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "Lawnmower"), 3, "Should have 3 Lawnmower");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "Phone Case"), 2, "Should have 2 Phone Case");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "TShirt"), 3, "Should have 3 TShirts");
        }

        [TestMethod]
        public void TestListInUSDollars()
        {
            //Act
            Assert.AreEqual(_productService.List().Count, 8, "Should bet 8 items");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "Lawnmower"), 3, "Should have 3 Lawnmower");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "Phone Case"), 2, "Should have 2 Phone Case");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "TShirt"), 3, "Should have 3 TShirts");

            var items = _productService.List();
            var itemsInUSDollars = _productService.ListInUSDollars();

            for (int i = 0; i < itemsInUSDollars.Count; i++)
            {
                Assert.AreEqual(itemsInUSDollars[i].Price, items[i].Price * 0.76, "US Rate should be applied");
            }
        }

        [TestMethod]
        public void TestListInEuro()
        {
            //Act
            Assert.AreEqual(_productService.List().Count, 8, "Should bet 8 items");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "Lawnmower"), 3, "Should have 3 Lawnmower");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "Phone Case"), 2, "Should have 2 Phone Case");
            Assert.AreEqual(_productService.List().Count(i => i.Type == "TShirt"), 3, "Should have 3 TShirts");

            var items = _productService.List();
            var itemsInEuro = _productService.ListInEuro();

            for (int i = 0; i < itemsInEuro.Count; i++)
            {
                Assert.AreEqual(itemsInEuro[i].Price, items[i].Price * 0.67, "US Rate should be applied");
            }
        }
    }
}
