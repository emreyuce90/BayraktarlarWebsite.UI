using AutoMapper;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class LetController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILetService _letService;
        private readonly IToastNotification _toastNotification;
        public LetController(ILetService letService, IToastNotification toastNotification, UserManager<User> userManager)
        {
            _letService = letService;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //oturum açan kullanıcı
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            bool IsAdmin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");
            if (IsAdmin)
            {
                ViewBag.IsAdmin = true;
                return View(await _letService.GetAllAsync());
            }
            else
            {
                ViewBag.IsAdmin = false;

                var vm = new EmployeeLetDetails();
                //Çalışma senesi
                vm.WorkYear = DateTime.Now.Year - loggedInUser.EntryDate.Year;
                //Hakettiği izinler
                if (DateTime.Now.Year - loggedInUser.EntryDate.Year > 1 || DateTime.Now.Year - loggedInUser.EntryDate.Year <= 5)
                {
                    vm.ThisYearLetRight = 14;
                }
                else if(DateTime.Now.Year - loggedInUser.EntryDate.Year > 5 || DateTime.Now.Year - loggedInUser.EntryDate.Year <= 15)
                {
                    vm.ThisYearLetRight = 20;
                }
                else if(DateTime.Now.Year - loggedInUser.EntryDate.Year >15)
                {
                    vm.ThisYearLetRight = 26;

                }
                else
                {
                    vm.ThisYearLetRight = 0;
                }

                //geçen yıl izin hakkı
                if ((DateTime.Now.Year - 1) - loggedInUser.EntryDate.Year > 1 || DateTime.Now.Year - loggedInUser.EntryDate.Year <= 5)
                {
                    vm.LastYearLetRight = 14;
                }
                else if ((DateTime.Now.Year-1) - loggedInUser.EntryDate.Year > 5 || DateTime.Now.Year - loggedInUser.EntryDate.Year <= 15)
                {
                    vm.LastYearLetRight = 20;
                }
                else if ((DateTime.Now.Year - 1) - loggedInUser.EntryDate.Year > 15)
                {
                    vm.LastYearLetRight = 26;

                }
                else
                {
                    vm.LastYearLetRight = 0;
                }

               //Geçen yıl kullanılan izinler
            };
            return View(await _letService.GetAllByUserIdAsync(loggedInUser.Id));
        }

        [HttpGet]
        public IActionResult LetAdd()
        {
            return View(new LetAddDto());
        }

        [HttpPost]
        public async Task<IActionResult> LetAdd(LetAddDto letAddDto)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                letAddDto.UserId = loggedInUser.Id;
                await _letService.AddLetAsync(letAddDto);
                _toastNotification.AddSuccessToastMessage("İzin talebi oluşturma işlemi başarılı", new ToastrOptions
                {
                    Title = "İşlem Başarılı"
                });
                return RedirectToAction("Index");
            }
            return View(letAddDto);
        }
    }
}
