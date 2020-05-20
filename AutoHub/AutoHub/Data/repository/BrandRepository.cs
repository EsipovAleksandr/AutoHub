using AutoHub.Data.Entities;
using AutoHub.Interfaces;
using AutoHub.ViewModels.Brand;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Data.repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool BrandExists(int id)
        {
            return _context.CarModels.Any(e => e.Id == id);
        }

        public async Task CreateAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<BrandViewModel> GetBrandAsync(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);

            var result = new BrandViewModel
            {
                Name = brand.Name
            };

            return result;
        }

        public List<BrandViewModel> GetBrandList()
        {
            var result = _context.Brands.Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return result.ToList();
        }


        public async Task UpdateAsync(Brand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
         }    
    }
}
