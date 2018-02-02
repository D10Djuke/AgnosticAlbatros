using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class RegisterData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public IEnumerable<Country> Countries { get; set; }
        public int Country { get; set; }

        public string ZipCode { get; set; }
        public string City { get; set; }

        public string Email { get; set; }
        public string PassWord { get; set; }
        public string PassWordRepeat { get; set; }
    }
}
