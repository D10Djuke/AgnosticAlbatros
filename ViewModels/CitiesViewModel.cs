using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class CitiesViewModel
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}
