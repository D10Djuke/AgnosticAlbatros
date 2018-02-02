using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgnosticAlbatros.ViewModels;
using AgnosticAlbatros.Models;
using AgnosticAlbatros.Services;

namespace AgnosticAlbatros.Controllers
{
    public class PlanningController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public PlanningController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            PlanningViewModel vm = new PlanningViewModel()
            {
                Kitchen = _db.Kitchens.First(x => x.ID == _userService.User.KitchenID).Name,
                Today = DateTime.Now.ToShortDateString()
            };

            return View(vm);
        }
    }
}