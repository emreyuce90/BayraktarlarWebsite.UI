using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ITahsilatService _tahsilatService;
        private readonly ICiroService _ciroService;
        private readonly UserManager<User> _userManager;
        public DashboardController(ICiroService ciroService, UserManager<User> userManager, ITahsilatService tahsilatService)
        {
            _ciroService = ciroService;
            _userManager = userManager;
            _tahsilatService = tahsilatService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? userId, int year = 2022)
        {

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            bool isAdmin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");
            if (isAdmin)
            {
                var allUsers = await _userManager.Users.Select(u => new UsernameAndIdVM
                {
                    Id = u.Id,
                    Name = u.FirstName

                }).ToListAsync();
                
                ViewBag.IsAdmin = true;
                TahsilatListDto alltahsilatList;
                CiroListDto allciroList;

                if (userId != null)
                {
                     alltahsilatList = await _tahsilatService.GetAllAsyncByUserId((int)userId, year);
                     allciroList = await _ciroService.GetCiroListAsync((int)userId, year);
                }
                else
                {
                     alltahsilatList = await _tahsilatService.GetAllAsync(year);
                     allciroList = await _ciroService.GetCiroListAsync(year);
                }
                

                CiroTahsilatVM m = new CiroTahsilatVM()
                {
                    Cirolar = allciroList,
                    Tahsilatlar = alltahsilatList,
                    SelectedYear = year,
                    UserNameAndId = allUsers,
                    SelectedUserId =userId
                };

                return View(m);
            }
            ViewBag.IsAdmin = false;
            var tahsilatList = await _tahsilatService.GetAllAsyncByUserId(loggedInUser.Id, year);
            var ciroList = await _ciroService.GetCiroListAsync(loggedInUser.Id, year);

            CiroTahsilatVM model = new CiroTahsilatVM()
            {
                Cirolar = ciroList,
                Tahsilatlar = tahsilatList,
                SelectedYear = year
            };

            return View(model);
        }
    }
}
