using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var identityResult = await _userManager.CreateAsync(user, model.Password.ToString());
                if (identityResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        ViewBag.Message = "Kullanıcı Ekleme İşlemi Başarılı";
                        return RedirectToAction("Login", "Auth");
                    }
                    foreach (var item in roleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }
                else
                {
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View(model);
                    }

                }

            }


            return View(model);

        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //tüm kullanıcılar
            var userList = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = userList
            });
        }
    }
}
