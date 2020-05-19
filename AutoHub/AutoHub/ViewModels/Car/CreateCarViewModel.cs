using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.ViewModels.Car
{
    public class CreateCarViewModel
    {
        public string BodyType { get; set; }

        public int ColorId { get; set; }

        public decimal Price { get; set; }

        public int CarModelId { get; set; }
    }
}
