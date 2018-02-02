using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class CityData
    {
        public City city { get; set; }
        public IEnumerable<Country> countres { get; set; }

        public long CountryId { get; set; }
    }
}
