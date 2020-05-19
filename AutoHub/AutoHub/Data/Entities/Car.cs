using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BodyType { get; set; }

        public Color Color { get; set; }

        public decimal Price { get; set; }

        public CarModel CarModel { get; set; }

    }
}
