using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ILetService _letService;
        private readonly UserManager<User> _userManager;
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService, UserManager<User> userManager, ILetService letService, ITicketService ticketService)
        {
            _customerService = customerService;
            _userManager = userManager;
            _letService = letService;
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            //giriş yapan kullanıcı
            var authenticatedUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            //eğer giriş yapan kullanıcı admin ise userid parametresini null geç
            var isAdmin=await _userManager.IsInRoleAsync(authenticatedUser,"Admin");
            if (isAdmin)
            {
                var model = new HomePageViewModel
                {
                    OnaydaBekleyenler = await _customerService.CountAsync(1),
                    Onaylananlar = await _customerService.CountAsync(2),
                    ReddedilenTabelalar = await _customerService.CountAsync(6),
                    UygulananTabelalar = await _customerService.CountAsync(5),
                    
                };
                return View(model);
            }
            else
            {
                
                var model = new HomePageViewModel
                {
                    OnaydaBekleyenler = await _customerService.CountAsync(1, authenticatedUser.Id),
                    Onaylananlar = await _customerService.CountAsync(2, authenticatedUser.Id),
                    ReddedilenTabelalar = await _customerService.CountAsync(6, authenticatedUser.Id),
                    UygulananTabelalar = await _customerService.CountAsync(5, authenticatedUser.Id),
                    WorkingYear = await _letService.WorkingYearAsync(authenticatedUser.Id),
                    RemainingLets = await _letService.RemainingLetAsync(authenticatedUser.Id),
                    UsedLastYear = await _letService.UsedLetsAsync((DateTime.Now.Year-1),authenticatedUser.Id),
                    UsedThisYear= await _letService.UsedLetsAsync(DateTime.Now.Year,authenticatedUser.Id),
                    Done = await _ticketService.CountClosedTicketsAsync(authenticatedUser.Id),
                    Todo = await _ticketService.CountNotClosedTicketsAsync(authenticatedUser.Id),
                    Planned= await _ticketService.CountRemainderTicketsAsync(authenticatedUser.Id)
                };
                return View(model);
            }
            
            
        }
    }
}
