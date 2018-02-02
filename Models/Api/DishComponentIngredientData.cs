using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class DishComponentIngredientData
    {
        public DishComponent DishComponent { get; set; }
        public DishComponentIngredientCoupling Coupling { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
