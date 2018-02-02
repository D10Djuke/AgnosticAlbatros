using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class IngredientsData
    {
        public Ingredient Ingredient { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}
