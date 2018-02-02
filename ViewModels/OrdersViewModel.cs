using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class OrdersViewModel
    {
        public User User { get; set; }
        public Company Company { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public string CreatedDate { get; set; }
    }
}
