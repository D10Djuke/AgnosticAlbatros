using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class UserData
    {
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }

        public IEnumerable<Kitchen> Kitchens { get; set; }
        public IEnumerable<UserTitle> UserTitles { get; set; }

        public long KitchenID { get; set; }
        public long UserTitleID { get; set; }

        public string NewKitchenName { get; set; }
        public string NewKitchenDescription { get; set; }
        public string NewUserTitle { get; set; }
        public string NewUserTitleDescription { get; set; }
    }
}
