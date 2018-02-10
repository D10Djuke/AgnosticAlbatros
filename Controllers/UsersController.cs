using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgnosticAlbatros.Models;
using AgnosticAlbatros.ViewModels;
using AgnosticAlbatros.Services;
using AgnosticAlbatros.Models.Api;
using AgnosticAlbatros.Helpers;

namespace AgnosticAlbatros.Controllers
{
    public class UsersController : Controller
    {
        private readonly DeliContext _db;
        private IUserService _userService;

        public UsersController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _db.Users.Where(x => x.Archived == false).ToList();
            var userTitles = _db.UserTitles.Where(x => x.CompanyId == _userService.User.CompanyID && x.Archived == false).ToList();

            var vm = new UsersViewModel()
            {
                Users = users,
                UserTitles = userTitles,
                UserTitle = "Title",
                FirstName = "FirstName",
                LastName = "LastName",
                LastSeenDate = "LastSeen"
            };

            return View(vm);
        }

        public IActionResult Create()
        {
            UserData data = new UserData()
            {
                UserTitles = _db.UserTitles.Where(x => x.CompanyId == _userService.User.CompanyID),
                Kitchens = _db.Kitchens.Where(x => x.CompanyID == _userService.User.CompanyID)
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Tel1,Tel2,KitchenID,UserTitleID,Email,NewKitchenName,NewKitchenDescription,NewUserTitle,NewUserTitleDescription")] UserData data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(data.NewKitchenName != null && !String.IsNullOrEmpty(data.NewKitchenName))
                    {
                        Kitchen kitchen = new Kitchen()
                        {
                            Name = data.NewKitchenName,
                            Description = data.NewKitchenDescription,
                            CompanyID = _userService.User.CompanyID,
                            CreatedAt = DateTime.UtcNow,
                            Guid = Guid.NewGuid()
                        };

                        _db.Add(kitchen);
                        await _db.SaveChangesAsync();
                    }
                    else if (data.NewUserTitle != null && !String.IsNullOrEmpty(data.NewUserTitle))
                    {
                        UserTitle title = new UserTitle()
                        {
                            Title = data.NewUserTitle,
                            Description = data.NewUserTitleDescription,
                            CompanyId = _userService.User.CompanyID,
                            CreatedAt = DateTime.UtcNow,
                            Guid = Guid.NewGuid()
                        };

                        _db.Add(title);
                        await _db.SaveChangesAsync();
                    }
                    else if (data.Email != null && !String.IsNullOrEmpty(data.Email))
                    {
                        var guid = Guid.NewGuid();

                        User user = new User()
                        {
                            Admin = false, 
                            CreatedAt = DateTime.UtcNow,
                            FirstName = data.FirstName,
                            LastName = data.LastName,
                            UserTitleID = data.UserTitleID,
                            CompanyID = _userService.User.CompanyID,
                            KitchenID = data.KitchenID,
                            Guid = guid,
                            Email = data.Email,
                            PassWord = EncryptionHelper.Encrypt("DEFAULT", data.Email)
                        };

                        _db.Add(user);
                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator. " + e);
            }

            data.Kitchens = _db.Kitchens.Where(x => x.CompanyID == _userService.User.CompanyID);
            data.UserTitles = _db.UserTitles.Where(x => x.CompanyId == _userService.User.CompanyID);

            data.NewKitchenDescription = "";
            data.NewKitchenName = "";
            data.NewUserTitle = "";
            data.NewUserTitleDescription = "";

            return View(data);
        }

        public IActionResult Details(long id)
        {
            User user = _db.Users.First(x => x.ID == id);

            user.PassWord = "DEFAULT";

            UserDetailData data = new UserDetailData()
            {
                User = user,
                Kitchens = _db.Kitchens.Where(x => x.CompanyID == _userService.User.CompanyID),
                UserTitles = _db.UserTitles.Where(x => x.CompanyId == _userService.User.CompanyID)
            };

            return View("Detail", data);
        }

        public async Task<IActionResult> Delete(long id)
        {
            User user = _db.Users.First(x => x.ID == id);

            if (!user.Admin)
            {
                user.Archived = true;
                _db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("", "Admin can't be deleted");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update([Bind("ID, Email, LastName, FirstName, KitchenID, UserTitleID, Tel1, Tel2")] User user)
        {
            User updatedUser = _db.Users.First(x => x.ID == user.ID);

            updatedUser.Email = user.Email;
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.KitchenID = user.KitchenID;
            updatedUser.UserTitleID = user.UserTitleID;
            updatedUser.Tel1 = user.Tel1;
            updatedUser.Tel2 = user.Tel2;

            updatedUser.UpdatedAt = DateTime.UtcNow;

            _db.Entry(updatedUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}