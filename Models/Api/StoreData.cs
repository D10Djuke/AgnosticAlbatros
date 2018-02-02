using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class StoreData
    {
        public Store Store { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
