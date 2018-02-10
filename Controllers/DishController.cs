using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgnosticAlbatros.Services;
using AgnosticAlbatros.Models;
using AgnosticAlbatros.ViewModels;
using AgnosticAlbatros.Models.Api;

namespace AgnosticAlbatros.Controllers
{
    public class DishController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public DishController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            DishViewModel vm = new DishViewModel()
            {
                Dishes = _db.Dishes.ToList()
            };

            return View(vm);
        }

        public IActionResult Create()
        {
            DishData data = new DishData()
            {
                Kitchens = _db.Kitchens.ToList()
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PricePerPerson,Preparation,KitchenID,DEscription, Notes")]Dish dish){      
            try
            {
                if (dish.Name != null && !String.IsNullOrEmpty(dish.Name))
                {
                    dish.Guid = Guid.NewGuid();
                    dish.CreatedAt = DateTime.UtcNow;

                    _db.Add(dish);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists " +
                                "see your system administrator. " + e);
            }

            DishData data = new DishData()
            {
                Kitchens = _db.Kitchens.ToList(),
                Dish = dish
            };

            return View(data);
        }
    }
}