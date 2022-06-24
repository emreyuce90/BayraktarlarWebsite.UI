﻿using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{

    public class UsersController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _toastNotification = toastNotification;
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

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //ilk olarak sana gelen eski şifreyi kontrol et bakalım eski şifre doğru mu?
                var user =await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                bool isValid = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                //eğer kullanıcının girdiği şifre doğru ise
                if (isValid)
                {
                   var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        //ilk olarak securitstampi değiştir
                        await _userManager.UpdateSecurityStampAsync(user);
                        //Kullanıcının oturumunu kapat
                        await _signInManager.SignOutAsync();
                        //Kullanıcıya yeni şifreyle tekrar oturum açtır
                        await _signInManager.PasswordSignInAsync(user,model.NewPassword, true, false);
                        _toastNotification.AddSuccessToastMessage("Şifre değiştirme işlemi başarılı", new ToastrOptions
                        {
                            Title = "İşlem başarılı"
                        });
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Eski şifrenizi yanlış girdiniz", new ToastrOptions
                    {
                        Title = "Hata oluştu"
                    });
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
