using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class ClientsData
    {
        public Client Client { get; set; }
        public IEnumerable<City> Cities { get; set; }

        public long CityID { get; set; }
    }
}
