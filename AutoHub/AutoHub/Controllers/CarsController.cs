using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoHub.Data;
using AutoHub.Data.Entities;
using AutoHub.ViewModels.Car;

namespace AutoHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarViewModel>>> GetCars()
        {
            var cars=_context.Cars.Include(x=>x.CarModel).ThenInclude(y=>y.Brand).Select(c =>
                                                     new CarViewModel()
                                                     {
                                                         Id =c.Id,
                                                         Name=c.Name,
                                                         BodyType=c.BodyType,
                                                         Color=c.Color.Name,
                                                         Brand=c.CarModel.Brand.Name,
                                                         Model=c.CarModel.Name,
                                                         Price=c.Price
                                                     }).ToListAsync();
            return await cars;
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarViewModel>> GetCar(int id)
        {
            var car = await _context.Cars.Include(x => x.CarModel).ThenInclude(y => y.Brand).FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            var result = new CarViewModel
            {
                Id = car.Id,
                Name = car.Name,
                BodyType = car.BodyType,
                Color = car.Color.Name,
                Brand = car.CarModel.Brand.Name,
                Model = car.CarModel.Name,
                Price = car.Price
            };
       
            return  result;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CreateCarViewModel carViewModel)
        {
            var carModel = await _context.CarModels.Include(x=>x.Brand).FirstOrDefaultAsync(x=>x.Id==carViewModel.CarModelId);
            var color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == carViewModel.ColorId);
            var newCar = new Car
            {
                Name = $"{carModel.Brand.Name} { carModel.Name}",
                BodyType= carViewModel.BodyType,
                Price=carViewModel.Price,
                CarModel= carModel,
                Color=color,
            };
            _context.Cars.Add(newCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = newCar.Id }, newCar);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
