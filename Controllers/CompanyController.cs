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
    public class CompanyController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public CompanyController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var company = _db.Companies.First(x => x.ID == _userService.User.CompanyID);
            var kitchens = _db.Kitchens.Where(x => x.CompanyID == company.ID && x.Archived == false).ToList();
            var u = _db.Users.Single(x => x.Admin == true && x.CompanyID == company.ID);

            var vm = new CompanyViewModel()
            {
                Compnay = company,
                Kitchens = kitchens,
                owner = u.FirstName + u.LastName,
                userCount = _db.Users.Count(x => x.CompanyID == company.ID && x.Archived == false)
            };

            return View(vm);
        }

        public async Task<IActionResult> Delete(long id)
        {
            Kitchen kitchen = _db.Kitchens.First(x => x.ID == id);

            kitchen.Archived = true;
            _db.Entry(kitchen).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Kitchen kitchen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (kitchen.Name != null && !String.IsNullOrEmpty(kitchen.Name))
                    {
                        kitchen.Guid = Guid.NewGuid();
                        kitchen.CreatedAt = DateTime.UtcNow;
                        kitchen.CompanyID = _userService.User.CompanyID;

                        _db.Add(kitchen);
                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
            }

            return View(kitchen);
        }

        public IActionResult DetailCompany(long id)
        {
            Company company = _db.Companies.First(x => x.ID == id);

            CompanyData data = new CompanyData()
            {
                Company = company,
                Cities = _db.Cities.ToList(),
                Countries = _db.Countries.ToList(),
                CountryID = _db.Cities.Single(x => x.ID == company.CityID).CountryID
            };

            return View("DetailCompany", data);
        }

        public IActionResult DetailKitchen(long id)
        {
            Kitchen kitchen = _db.Kitchens.First(x => x.ID == id);

            return View("DetailKitchen", kitchen);
        }

        public async Task<IActionResult> UpdateCompany([Bind("ID,Name,VAT,AddressStreet,AddressNumber,CityID,Tel1,Tel2")]Company company)
        {
            Company updatedCompany = _db.Companies.First(x => x.ID == company.ID);
            updatedCompany.Name = company.Name;
            updatedCompany.VAT = company.VAT;
            updatedCompany.AddressStreet = company.AddressStreet;
            updatedCompany.AddressNumber = company.AddressNumber;
            updatedCompany.CityID = company.CityID;
            updatedCompany.Tel1 = company.Tel1;
            updatedCompany.Tel2 = company.Tel2;

            updatedCompany.UpdatedAt = DateTime.UtcNow;

            _db.Entry(updatedCompany).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateKitchen([Bind("ID,Name,Description")]Kitchen kitchen)
        {
            Kitchen updatedKitchen = _db.Kitchens.First(x => x.ID == kitchen.ID);

            updatedKitchen.Name = kitchen.Name;
            updatedKitchen.Description = kitchen.Description;
            updatedKitchen.UpdatedAt = DateTime.UtcNow;

            _db.Entry(updatedKitchen).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}