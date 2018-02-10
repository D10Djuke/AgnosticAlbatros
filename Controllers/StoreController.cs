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
    public class StoreController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public StoreController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            StoreViewModel vm = new StoreViewModel()
            {
                Cities = _db.Cities.ToList(),
                Stores = _db.Stores.Where(x => x.Archived == false)
            };

            return View(vm);
        }

        public async Task<IActionResult> Delete(long id)
        {
            Store store = _db.Stores.First(x => x.ID == id);

            store.Archived = true;
            _db.Entry(store).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            StoreData data = new StoreData()
            {
                Cities = _db.Cities.ToList()
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Tel1,Tel2,AddressStreet,AddressNumber,CityID,Notes")]Store store)
        {
            try
            {
                if (store.Name != null && !String.IsNullOrEmpty(store.Name))
                {
                    store.Guid = Guid.NewGuid();
                    store.CreatedAt = DateTime.UtcNow;

                    _db.Add(store);
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

            StoreData data = new StoreData()
            {
                Cities = _db.Cities.ToList(),
                Store = store
            };

            return View(data);
        }

        public IActionResult Detail(long id)
        {
            Store store = _db.Stores.First(x => x.ID == id);

            StoreData data = new StoreData()
            {
                Cities = _db.Cities.ToList(),
                Store = store
            };

            return View("Detail", data);
        }

        public async Task<IActionResult> Update([Bind("ID,Name,TEL1,TEL2,AddressStreet,AddressNumber,CityID")]Store store)
        {
            Store updatedStore = _db.Stores.First(x => x.ID == store.ID);

            updatedStore.Name = store.Name;
            updatedStore.Tel1 = store.Tel1;
            updatedStore.Tel2 = store.Tel2;
            updatedStore.AddressNumber = store.AddressNumber;
            updatedStore.AddressStreet = store.AddressStreet;
            updatedStore.CityID = store.CityID;

            updatedStore.UpdatedAt = DateTime.UtcNow;

            _db.Entry(updatedStore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}