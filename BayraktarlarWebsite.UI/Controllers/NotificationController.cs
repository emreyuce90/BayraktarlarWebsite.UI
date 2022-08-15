using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        public NotificationController(INotificationService notificationService, UserManager<User> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var loggedInUser =await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var allNotifications = await _notificationService.GetAllAsync(loggedInUser.Id);
            return View(allNotifications);
        }
        [HttpPost]
        public async Task<IActionResult> Read(int notificationId)
        {
            if(notificationId != 0)
            {
               await _notificationService.ReadAsync(notificationId);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
            
        }

    }
}
