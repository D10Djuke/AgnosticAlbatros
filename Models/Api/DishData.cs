using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class DishData
    {
        public Dish Dish { get; set; }
        public IEnumerable<Kitchen> Kitchens { get; set; }
    }
}
