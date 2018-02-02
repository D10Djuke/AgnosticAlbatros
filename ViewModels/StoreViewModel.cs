using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class StoreViewModel
    {
        public IEnumerable<Store> Stores { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
