using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class TicketController : Controller
    {
        private readonly IUrgencyService _urgencyService;
        private readonly UserManager<User> _userManager;
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService, UserManager<User> userManager, IUrgencyService urgencyService)
        {
            _ticketService = ticketService;
            _userManager = userManager;
            _urgencyService = urgencyService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await _ticketService.GetAllAsync(loggedInUser.Id));
        }

        [HttpGet]
        public async Task<IActionResult> AddTicket()
        {
            //Aciliyet tablosunu çek
            var aciliyetler = await _urgencyService.GetAllAsync();
            ViewBag.Select = new SelectList(aciliyetler.Urgencies,"Id","UrgencyName");
            return View(new TicketAddDto());
        }
    }
}
