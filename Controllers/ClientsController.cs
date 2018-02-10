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
    public class ClientsController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public ClientsController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            ClientsViewModel vm = new ClientsViewModel()
            {
                Cities = _db.Cities.ToList(),
                Clients = _db.Clients.Where(x => x.Archived == false && x.CompanyID == _userService.User.CompanyID).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> Delete(long id)
        {
            Client client = _db.Clients.First(x => x.ID == id);

            client.Archived = true;
            _db.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Create()
        {
            ClientsData data = new ClientsData()
            {
                Cities = _db.Cities.ToList()
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,VAT,Tel1,Tel2,AddressStreet,AddressNumber,CityID")]Client client)
        {
            try
            {
                if (client.Name != null && !String.IsNullOrEmpty(client.Name))
                {
                    client.Guid = Guid.NewGuid();
                    client.CompanyID = _userService.User.CompanyID;
                    client.CreatedAt = DateTime.UtcNow;

                    _db.Add(client);
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

            ClientsData data = new ClientsData()
            {
                Cities = _db.Cities.ToList(),
                Client = client
            };

            return View(data);
        }

        public IActionResult Detail(long id)
        {
            Client client = _db.Clients.First(x => x.ID == id);

            ClientsData data = new ClientsData()
            {
                Cities = _db.Cities.ToList(),
                Client = client
            };

            return View("Detail", data);
        }

        public async Task<IActionResult> Update([Bind("ID,Name, VAT,TEL1,TEL2,AddressStreet,AddressNumber,CityID")]Client client)
        {
            Client updatedClient = _db.Clients.First(x => x.ID == client.ID);

            updatedClient.Name = client.Name;
            updatedClient.VAT = client.VAT;
            updatedClient.Tel1 = client.Tel1;
            updatedClient.Tel2 = client.Tel2;
            updatedClient.AddressNumber = client.AddressNumber;
            updatedClient.AddressStreet = client.AddressStreet;

            updatedClient.UpdatedAt = DateTime.UtcNow;

            _db.Entry(updatedClient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}