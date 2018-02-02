using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgnosticAlbatros.Models;
using AgnosticAlbatros.Services;
using AgnosticAlbatros.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgnosticAlbatros.Controllers
{
    public class OrdersController : Controller
    {
        private IUserService _userService;
        private DeliContext _db;

        public OrdersController(DeliContext dbContext, IUserService userService)
        {
            _db = dbContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            Company company = _db.Companies.First(x => x.ID == _userService.User.CompanyID);

            OrdersViewModel vm = new OrdersViewModel()
            {
                User = _userService.User,
                Company = company,
                CreatedDate = "Created At",
                Orders = null
            };

            return View(vm);
        }
    }
}