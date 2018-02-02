using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Services
{
    public class UserService : IUserService
    {
        private User _user = null;
        public User User { get => _user; set => _user = value; }
    }
}
