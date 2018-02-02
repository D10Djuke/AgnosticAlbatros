using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class CompanyData
    {
        public Company Company { get; set; }
        public IEnumerable<Country> Countries {get;set;}
        public IEnumerable<City> Cities { get; set; }

        public long CountryID { get; set; }
        
    }
}
