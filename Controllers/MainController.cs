using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgnosticAlbatros.Models;
using AgnosticAlbatros.Models.Api;
using AgnosticAlbatros.Helpers;
using AgnosticAlbatros.Services;

namespace AgnosticAlbatros.Controllers
{
    public class MainController : Controller
    {
        private IUserService _userService;
        private readonly DeliContext _db;

        public MainController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,PassWord")]LoginData data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User u = _db.Users.FirstOrDefault(x => x.Email == data.Email);

                    if (u != null)
                    {
                        if ((EncryptionHelper.Decrypt(u.PassWord, u.Email)).Equals(data.PassWord))
                        {
                            u.LastSeen = DateTime.UtcNow;
                            await _db.SaveChangesAsync();

                            _userService.User = u;

                            return RedirectToAction("Index", u.Admin ? "Orders" : "Planning");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Email en wachtwoordkomen niet overeen.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email is niet geassocieerd met een Deligate-account.");
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator. " + e);
            }

            return View("Index", data);
        }

        public IActionResult Register()
        {
            RegisterData data = new RegisterData()
            {
                Countries = _db.Countries.ToList()
            };

            return View("Register", data);
        }

        public IActionResult About()
        {
            return View("About");
        }

        public async Task<IActionResult> RegisterForm([Bind("Email,FirstName,LastName,CompanyName,Country,ZipCode,City")]RegisterData data)
        {
            using (var dbContextTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        DateTime now = DateTime.UtcNow;

                        City city = _db.Cities.FirstOrDefault(x => x.Name == data.City && x.ZipCode == data.ZipCode && x.CountryID == data.Country);

                        if (city == null)
                        {
                            city = new City()
                            {
                                CountryID = data.Country,
                                Guid = Guid.NewGuid(),
                                Name = data.City,
                                ZipCode = data.ZipCode
                            };
                            _db.Add(city);
                            await _db.SaveChangesAsync();
                        }

                        Company company = _db.Companies.FirstOrDefault(x => x.Name == data.CompanyName);

                        if (company == null)
                        {
                            company = new Company()
                            {
                                Name = data.CompanyName,
                                CreatedAt = now,
                                CityID = city.ID,
                                Guid = Guid.NewGuid()
                            };
                            _db.Add(company);

                            await _db.SaveChangesAsync();

                            UserTitle title = new UserTitle()
                            {
                                CreatedAt = now,
                                Title = "Owner",
                                Guid = Guid.NewGuid(),
                                CompanyId = company.ID
                            };

                            _db.Add(title);

                            Kitchen kitchen = new Kitchen()
                            {
                                CompanyID = company.ID,
                                Guid = Guid.NewGuid(),
                                CreatedAt = now,
                                Name = "DEFAULT"
                            };

                            _db.Add(kitchen);
                            await _db.SaveChangesAsync();

                            User user = _db.Users.FirstOrDefault(x => x.Email == data.Email);

                            string generatedPassWord = EncryptionHelper.GeneratePassword(12, (new Random()).Next(2, 6));

                            if (user == null)
                            {
                                if ((data.PassWord).Equals(data.PassWordRepeat))
                                {
                                    user = new User()
                                    {
                                        Admin = true,
                                        CreatedAt = now,
                                        FirstName = data.FirstName,
                                        LastName = data.LastName,
                                        UserTitleID = title.ID,
                                        CompanyID = company.ID,
                                        KitchenID = kitchen.ID,
                                        Guid = Guid.NewGuid(),
                                        Email = data.Email,
                                        PassWord = EncryptionHelper.Encrypt(generatedPassWord, data.Email)
                                    };

                                    _db.Add(user);
                                    await _db.SaveChangesAsync();
                                    dbContextTransaction.Commit();
                                    return RedirectToAction(nameof(Index));
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Passwords did not match");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Email already registered");
                            }

                            MailHelper.SendRegisterEmail(data.Email, generatedPassWord, data.FirstName + " " + data.LastName);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Company Already Exists");
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists " +
                                "see your system administrator. " + e);
                }

                dbContextTransaction.Rollback();
                data.Countries = _db.Countries.ToList();
            }

            return View("Register", data);
        }
    }
}