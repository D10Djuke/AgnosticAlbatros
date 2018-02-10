using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgnosticAlbatros.Models;
using AgnosticAlbatros.Services;
using AgnosticAlbatros.ViewModels;
using AgnosticAlbatros.Models.Api;

namespace AgnosticAlbatros.Controllers
{
    public class IngredientsController : Controller
    {

        private readonly DeliContext _db;
        private IUserService _userService;

        public IngredientsController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            IngredientsViewModel vm = new IngredientsViewModel()
            {
                Ingredients = _db.Ingredients.Where(x => x.Archived == false),
                Stores = _db.Stores.Where(x => x.Archived == false)
            };

            return View(vm);
        }
        public async Task<IActionResult> Delete(long id)
        {
            City city = _db.Cities.First(x => x.ID == id);

            _db.Remove(city);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            IngredientsData data = new IngredientsData()
            {
                Stores = _db.Stores.ToList()
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Notes,StoreID")]Ingredient ingredient)
        {
            try
            {
                if (ingredient.Name != null && !String.IsNullOrEmpty(ingredient.Name))
                {
                    ingredient.Guid = Guid.NewGuid();
                    ingredient.CreatedAt = DateTime.UtcNow;

                    _db.Add(ingredient);
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

            IngredientsData data = new IngredientsData()
            {
                Ingredient = ingredient,
                Stores = _db.Stores.ToList()
            };

            return View(data);
        }

        public IActionResult Detail(long id)
        {
            Ingredient ingredient = _db.Ingredients.First(x => x.ID == id);

            IngredientsData data = new IngredientsData()
            {
                Ingredient = ingredient,
                Stores = _db.Stores.ToList()
            };

            return View("Detail", data);
        }

        public async Task<IActionResult> UpdateCity([Bind("ID,Name,Description,Notes,StoreID")]Ingredient ingredient)
        {
            Ingredient updatedIngredient = _db.Ingredients.First(x => x.ID == ingredient.ID);

            updatedIngredient.Name = ingredient.Name;
            updatedIngredient.Description = ingredient.Description;
            updatedIngredient.Notes = ingredient.Notes;
            updatedIngredient.StoreID = ingredient.StoreID;

            updatedIngredient.UpdatedAt = DateTime.UtcNow;

            _db.Entry(updatedIngredient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}