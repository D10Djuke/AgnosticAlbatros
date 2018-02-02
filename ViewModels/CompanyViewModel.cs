using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class CompanyViewModel
    {
        public Company Compnay { get; set; }
        public IEnumerable<Kitchen> Kitchens { get; set; }

        public int userCount { get; set; }
        public string owner { get; set; }
    }
}
