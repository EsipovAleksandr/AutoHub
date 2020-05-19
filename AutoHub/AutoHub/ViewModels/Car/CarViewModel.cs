using AutoHub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.ViewModels.Car
{
    public class CarViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BodyType { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public string Brand  { get; set; }

        public string Model { get; set; }
    }
}
