using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class IngredientsViewModel
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}
