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
        private readonly ICiroService _ciroService;
        private readonly UserManager<User> _userManager;
        public DashboardController(ICiroService ciroService, UserManager<User> userManager)
        {
            _ciroService = ciroService;
            _userManager = userManager;
            
        }
        [HttpGet]
        public async Task<IActionResult> Index(int year=2022)
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var list =await _ciroService.GetCiroListAsync(loggedInUser.Id, year);
            ViewBag.SelectedValue = year;
            return View(list);
        }
    }
}
