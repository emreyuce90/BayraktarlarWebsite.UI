using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService, UserManager<User> userManager)
        {
            _customerService = customerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var authenticatedUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var model = new HomePageViewModel
            {
                OnaydaBekleyenler = await _customerService.CountAsync(1,authenticatedUser.Id),
                Onaylananlar= await _customerService.CountAsync(2,authenticatedUser.Id),
                ReddedilenTabelalar = await _customerService.CountAsync(6, authenticatedUser.Id),
                UygulananTabelalar = await _customerService.CountAsync(5, authenticatedUser.Id)
            };
            return View(model);
        }
    }
}
