using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel
            {
                OnaydaBekleyenler = await _customerService.CountAsync(1),
                Onaylananlar= await _customerService.CountAsync(2),
                ReddedilenTabelalar = await _customerService.CountAsync(6),
                UygulananTabelalar = await _customerService.CountAsync(5)
            };
            return View(model);
        }
    }
}
