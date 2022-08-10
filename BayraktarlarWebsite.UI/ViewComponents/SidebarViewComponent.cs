using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.ViewComponents
{
    public class SidebarViewComponent:ViewComponent
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<User> _userManager;

        public SidebarViewComponent(UserManager<User> userManager, ITicketService ticketService)
        {
            _userManager = userManager;
            _ticketService = ticketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //giriş yapan kullanıcı
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            //giriş yapan kullanıcının rolleri
            var loggedInUserRoles = await _userManager.GetRolesAsync(loggedInUser);
            var model = new UserInfoViewModel
            {
                Username = loggedInUser.UserName,
                Unclosed = await _ticketService.CountNotClosedTicketsAsync(loggedInUser.Id),
                Closed = await _ticketService.CountClosedTicketsAsync(loggedInUser.Id),
                Remainder = await _ticketService.CountRemainderTicketsAsync(loggedInUser.Id),
                CountAssigneds = await _ticketService.CountAssignedTicketsAsync()
            };
            if (loggedInUserRoles.Contains("Admin"))
            {
                model.IsAdmin = true;
            }
            else
            {
                model.IsAdmin = false;

            }

            return View(model);
        }
    }
}
