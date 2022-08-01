using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
        public async Task<IActionResult> Remainder()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await _ticketService.GetAllFutureAsync(loggedInUser.Id));
        }

        [HttpGet]
        public async Task<IActionResult> ClosedTickets()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await _ticketService.ClosedTicketsAsync(loggedInUser.Id));
        }

        [HttpGet]
        public async Task<IActionResult> AddTicket()
        {
            //Aciliyet tablosunu çek
            var aciliyetler = await _urgencyService.GetAllAsync();
            ViewBag.Select = new SelectList(aciliyetler.Urgencies,"Id","UrgencyName");
            return View(new TicketAddDto() { RemainderDate=Convert.ToDateTime(DateTime.Now.ToString("f"))});
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketAddDto model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                model.CreatedDate = DateTime.Now;
                model.UserId = loggedInUser.Id;
                await _ticketService.AddTicketAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveTicket(int ticketId)
        {
            if(ticketId != 0)
            {
                await _ticketService.ApproveAsync(ticketId);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
