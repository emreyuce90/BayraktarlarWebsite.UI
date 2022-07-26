using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService, UserManager<User> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _ticketService.GetAllAsync());
        }
    }
}
