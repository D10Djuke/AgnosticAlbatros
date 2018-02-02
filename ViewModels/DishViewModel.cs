using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class DishViewModel
    {
        public IEnumerable<Dish> Dishes { get; set; }
    }
}
