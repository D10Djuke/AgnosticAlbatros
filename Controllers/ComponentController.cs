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
    public class ComponentController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public ComponentController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            ComponentViewModel vm = new ComponentViewModel()
            {
                Components = _db.DishComponents.ToList(),
                Ingredients = _db.Ingredients.ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> Delete(long id)
        {
            DishComponent component = _db.DishComponents.First(x => x.ID == id);

            _db.Remove(component);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCoupling(long id)
        {
            DishComponentIngredientCoupling component = _db.DishComponentIngredientCouplings.First(x => x.ID == id);

            _db.Remove(component);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create(long id)
        {
            ComponentData data = new ComponentData()
            {
                Ingredients = _db.Ingredients.ToList(),
                couplings = null
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PricePerPerson,Description,Notes,Preparation")]DishComponent component)
        {
            try
            {
                if (component.Name != null && !String.IsNullOrEmpty(component.Name))
                {
                    component.Guid = Guid.NewGuid();

                    _db.Add(component);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
            }

            ComponentData data = new ComponentData()
            {
                Component = component,
                Ingredients = _db.Ingredients.ToList()
            };

            return View(data);
        }

        public IActionResult Detail(long id)
        {
            DishComponent component = _db.DishComponents.First(x => x.ID == id);

            ComponentData data = new ComponentData()
            {
                Component = component,
                Ingredients = _db.Ingredients.ToList()
            };

            return View("Detail", data);
        }

        public async Task<IActionResult> Update([Bind("ID,Name,PricePerPerson,Description,Notes,Preparation")]DishComponent component)
        {
            DishComponent up = _db.DishComponents.First(x => x.ID == component.ID);

            up.Name = component.Name;
            up.Notes = component.Notes;
            up.Preparation = component.Preparation;
            up.PricePerPerson = component.PricePerPerson;
            up.Description = component.Description;

            _db.Entry(up).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Coupling(long id)
        {
            DishComponent component = _db.DishComponents.First(x => x.ID == id);

            DishComponentIngredientData data = new DishComponentIngredientData()
            {
                DishComponent = component,
                Ingredients = _db.Ingredients.ToList()
            };

            return View("Coupling", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCoupling([Bind("DichComponentID,Amount,Price,Notes,IngredientID")]DishComponentIngredientCoupling c)
        {
            try
            {

                c.Guid = Guid.NewGuid();

                _db.Add(c);
                await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
            }

            ComponentData data = new ComponentData()
            {
                Component = _db.DishComponents.Single(x => x.ID == c.DichComponentID),
                Ingredients = _db.Ingredients.ToList()
            };

            return View("Index", data);
        }
    }
}