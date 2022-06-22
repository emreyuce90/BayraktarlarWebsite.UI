using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Code);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifreniz yanlıştır");

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
