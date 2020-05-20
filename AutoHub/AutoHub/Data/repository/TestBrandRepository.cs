using AutoHub.Data.Entities;
using AutoHub.Interfaces;
using AutoHub.ViewModels.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Data.repository
{
    public class TestBrandRepository : IBrandRepository
    {
        public bool BrandExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Brand item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BrandViewModel> GetBrandAsync(int id)
        {
            return  GetList().FirstOrDefault(x => x.Id == id);
        }

        public List<BrandViewModel> GetBrandList()
        {           
            return GetList();
        }

        public Task UpdateAsync(Brand item)
        {
            throw new NotImplementedException();
        }

        private List<BrandViewModel> GetList()
        {
            List<BrandViewModel> ListBrand = new List<BrandViewModel>()
            {
                new BrandViewModel()
                {
                    Id = 1,
                    Name = "Audi"
                },
                new BrandViewModel()
                {
                    Id = 2,
                    Name = "BMV"
                }
            };

            return ListBrand;
        }
    
    }
}
