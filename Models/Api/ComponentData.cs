using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class ComponentData
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public DishComponent Component { get; set; }

        public IEnumerable<DishComponentIngredientCoupling> couplings { get; set; }
    }
}
