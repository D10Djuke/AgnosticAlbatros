using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class ClientsViewModel
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}
