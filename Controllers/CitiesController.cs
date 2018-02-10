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
    public class CitiesController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public CitiesController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            CitiesViewModel vm = new CitiesViewModel()
            {
                Cities = _db.Cities.ToList(),
                Countries = _db.Countries.ToList()
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
            CityData data = new CityData()
            {
                countres = _db.Countries.ToList()
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ZipCode,CountryID")]City city){
            try
            {
                if (city.Name != null && !String.IsNullOrEmpty(city.Name))
                {
                    city.Guid = Guid.NewGuid();

                    _db.Add(city);
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

            CityData data = new CityData()
            {
                city = city,
                countres = _db.Countries.ToList()
            };

            return View(data);
        }

        public IActionResult Detail(long id)
        {
            City city = _db.Cities.First(x => x.ID == id);

            CityData data = new CityData()
            {
                city = city,
                countres = _db.Countries.ToList()
            };

            return View("Detail", data);
        }

        public async Task<IActionResult> UpdateCity([Bind("ID,Name,ZipCode,CountryID")]City city)
        {
            City updatedCity = _db.Cities.First(x => x.ID == city.ID);

            updatedCity.Name = city.Name;
            updatedCity.ZipCode = city.ZipCode;
            updatedCity.CountryID = city.CountryID;

            _db.Entry(updatedCity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}