using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(new UserAddViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Code = model.Code,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    PhoneNumber = model.Mobile,
                    UserName = model.Code,
                    Profile = model.Picture
                };
                var identityResult = await _userManager.CreateAsync(user,model.Password.ToString());
                if (identityResult.Succeeded)
                {
                    ViewBag.Message = "Kullanıcı Ekleme İşlemi Başarılı";
                    return RedirectToAction("Success");
                }
                else
                {
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                        return View(model);
                    }
                   
                }
          
            }
       
           
                return View(model);
            
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
                if(user != null)
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
