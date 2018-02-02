using AgnosticAlbatros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Services
{
    public interface IUserService
    {
        User User { get; set; }
    }
}
