using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.ViewComponents
{
    public class UnclosedTicketComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly ITicketService _ticketService;

        public UnclosedTicketComponent(ITicketService ticketService, UserManager<User> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            int unclosedTodos =await _ticketService.CountNotClosedTicketsAsync(loggedInUser.Id);
            ViewBag.Unclosed = unclosedTodos;
            return View();
        }
    }
}
