using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoHub.Data;
using AutoHub.Data.Entities;
using AutoHub.ViewModels.Brand;
using System.Runtime.InteropServices;
using AutoHub.Interfaces;

namespace AutoHub.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _repository;

        public BrandsController(IBrandRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Brands
        [HttpGet]
        public  ActionResult<IEnumerable<BrandViewModel>> GetBrands()
        {
            var result = _repository.GetBrandList();

            return result;
        }

       // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandViewModel>> GetBrand(int id)
        {
            var brand = await _repository.GetBrandAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            var brandViewModel = new BrandViewModel
            {
                Name = brand.Name
            };
            return brandViewModel;
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }     
            try
            {
                await _repository.UpdateAsync(brand);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/Brands
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            await _repository.CreateAsync(brand);
            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteBrand(int id)
        {
            if (id<0)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);

            return id;
        }

        private bool BrandExists(int id)
        {
            return _repository.BrandExists(id);
        }
    }
}
