using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoHub.Data;
using AutoHub.Data.Entities;
using AutoHub.ViewModels;

namespace AutoHub.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarModelViewModel>>> GetCarModels()
        {
            var result=_context.CarModels.Include(x => x.Brand).Select(y=> 
                                                     new CarModelViewModel(){
                                                                              Id=y.Id,
                                                                              Name=y.Name,
                                                                              BrandName=y.Brand.Name                                                                      
                                                                             })
                                                                             .ToListAsync();
            return await result;
        }

        // GET: api/CarModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarModelViewModel>> GetCarModel(int id)
        {
            var carModel = await _context.CarModels.Include(x=>x.Brand).FirstOrDefaultAsync(x=>x.Id==id);

            if (carModel == null)
            {
                return NotFound();
            }
            var result = new CarModelViewModel
            {
                Id = carModel.Id,
                Name = carModel.Name,
                BrandName = carModel.Brand.Name
            };

            return result;
        }

        // PUT: api/CarModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarModel(int id, CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(carModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarModelExists(id))
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

        // POST: api/CarModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CarModel>> PostCarModel(CraeateCarModelViewModel carModel)
        {
            var brand=_context.Brands.FirstOrDefault(x => x.Id == carModel.BrandId);
            if (brand == null) return BadRequest("Brand not found");
            var newCarModel = new CarModel
            {
                Name = carModel.Name,
                Brand = brand
            };
            _context.CarModels.Add(newCarModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarModel", new { id = newCarModel.Id }, newCarModel);
        }

        // DELETE: api/CarModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            
            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.Id == id);
        }
    }
}
