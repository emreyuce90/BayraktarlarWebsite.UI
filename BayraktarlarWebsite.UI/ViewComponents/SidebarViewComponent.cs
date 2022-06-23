using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.ViewComponents
{
    public class SidebarViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;
        
        public SidebarViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //giriş yapan kullanıcı
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            //giriş yapan kullanıcının rolleri
            var loggedInUserRoles = await _userManager.GetRolesAsync(loggedInUser);
            var model = new UserInfoViewModel
            {
                Username = loggedInUser.UserName
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
