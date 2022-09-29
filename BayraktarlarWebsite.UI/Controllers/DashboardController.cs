using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Index(int year=2022)
        {
            
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
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
