using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.ViewModels
{
    public class UsersViewModel
    {
        public List<User> Users { get; set; }
        public List<UserTitle> UserTitles { get; set; }

        public string LastSeenDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserTitle { get; set; }
    }
}
