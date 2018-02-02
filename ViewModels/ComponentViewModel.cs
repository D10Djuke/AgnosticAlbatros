using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class ComponentViewModel
    {
        public IEnumerable<DishComponent> Components { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
