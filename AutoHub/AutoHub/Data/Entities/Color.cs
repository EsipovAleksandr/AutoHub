using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Data.Entities
{
    public class Color
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Car> Cars { get; set; }
    }
}
