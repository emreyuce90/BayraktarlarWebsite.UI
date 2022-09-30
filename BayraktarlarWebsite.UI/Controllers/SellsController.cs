using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class SellsController : Controller
    {
        private readonly ISellService _sellService;
        private readonly UserManager<User> _userManager;
        public SellsController(ISellService sellService, UserManager<User> userManager)
        {
            _sellService = sellService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId,int month=1,int year = 2022)
        {
            SellListDto satislar;
            IList<UsernameAndIdVM> users;
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            bool IsAdmin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");
            if (IsAdmin)
            {
                ViewBag.IsAdmin = true;
                users = await _userManager.Users.Where(u=>u.IsRepresentive == true).Select(u => new UsernameAndIdVM { Id = u.Id, Name = u.FirstName }).ToListAsync();
                satislar = await _sellService.GetAllByUserIdAsync(userId, year,month);
                return View(new SellTargetVM { SelectedYear = year, SellList = satislar, UserNameAndId = users ,SelectedMonth =month});
            }
            satislar = await _sellService.GetAllByUserIdAsync(loggedInUser.Id, year,month);
            return View(new SellTargetVM { SelectedYear = year, SellList = satislar,SelectedMonth =month });
        }
    }
}
