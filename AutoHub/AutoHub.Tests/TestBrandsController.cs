using AutoHub.Controllers;
using AutoHub.Data.repository;
using AutoHub.ViewModels.Brand;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AutoHub.Tests
{
    public class TestBrandsController
    {
        public TestBrandRepository brandRepository;
        public BrandsController brandsController;
        [SetUp]
        public void Setup()
        {
            brandRepository = new TestBrandRepository();
            brandsController = new BrandsController(brandRepository);
        }


        [Test]
        public void GetBrands_ShouldReturnAllBrandAsync()
        {
            var brands =  brandsController.GetBrands().Value;
            var actualList = GetTestBrand();
            Assert.AreEqual(brands.ToString(), actualList.ToString());
        }


        [Test]
        public async Task GetBrand_ShouldReturnNotEqual()
        {
            var brand= await brandsController.GetBrand(1);
            Assert.AreNotEqual("BMV",brand.Value.Name);
        }


        private List<BrandViewModel> GetTestBrand()
        {
            var testProducts = new List<BrandViewModel>();
            testProducts.Add(new BrandViewModel { Id = 1, Name = "Audi"});
            testProducts.Add(new BrandViewModel { Id = 2, Name = "BMV"});

            return testProducts;
        }
    }
}