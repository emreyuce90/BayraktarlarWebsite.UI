using AutoMapper;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Helpers.Abstract;
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
        private readonly ILetTimeCalculator _calculate;
        private readonly UserManager<User> _userManager;
        private readonly ILetService _letService;
        private readonly IToastNotification _toastNotification;
        public LetController(ILetService letService, IToastNotification toastNotification, UserManager<User> userManager, ILetTimeCalculator calculate)
        {
            _letService = letService;
            _toastNotification = toastNotification;
            _userManager = userManager;
            _calculate = calculate;
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
                var lets = await _letService.GetAllAsync();
                return View(new EmployeeLetDetails
                {
                    Lets = lets
                });
            }
            else
            {
                ViewBag.IsAdmin = false;
                var vm = new EmployeeLetDetails();
                //Çalışma senesi
                vm.WorkYear = DateTime.Now.Year - loggedInUser.EntryDate.Year;
                //Bu yıl hakettiği izinler
                vm.ThisYearLetRight = _calculate.CalculateLet(DateTime.Now.Year - loggedInUser.EntryDate.Year);

                //geçen yıl izin hakkı
                vm.LastYearLetRight = _calculate.CalculateLet((DateTime.Now.Year - 1) - loggedInUser.EntryDate.Year);

                //Geçen yıl kullanılan izinler
                vm.LastYearLetUsed = await _letService.UsedLetsAsync((DateTime.Now.Year - 1), loggedInUser.Id);
                //Bu yıl kullanılan izinler
                vm.ThisYearLetUsed = await _letService.UsedLetsAsync((DateTime.Now.Year), loggedInUser.Id);
                //Kalan İzin Hakkı
                vm.Balance = (vm.LastYearLetRight - vm.LastYearLetUsed) + (vm.ThisYearLetRight - vm.ThisYearLetUsed);
                var lets = await _letService.GetAllByUserIdAsync(loggedInUser.Id);
                vm.Lets = lets;
                return View(vm);
            };

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
                letAddDto.CreatedDate = DateTime.Now;
                //Eğer izin hakkı >= Talep edilenden
                //Bu yıl izin hakkı //Geçen yıl izin hakkı

                int remainingLets = await _letService.RemainingLetAsync(loggedInUser.Id);
                if (remainingLets >= letAddDto.DayCount)
                {


                    await _letService.AddLetAsync(letAddDto);
                    _toastNotification.AddSuccessToastMessage("İzin talebi oluşturma işlemi başarılı", new ToastrOptions
                    {
                        Title = "İşlem Başarılı"
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", $"Kullanabileceğiniz izin sayısı en fazla {remainingLets} olabilir. Kullanmak istediğiniz izin sayısı hakkettiğinizden fazla");
                    _toastNotification.AddErrorToastMessage("İzin talebi oluşturma işlemi başarısız", new ToastrOptions
                    {
                        Title = "İşlem Başarısız"
                    });
                    return View(letAddDto);
                }
            }
            return View(letAddDto);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveLet(int letId)
        {
            if (letId != 0)
            {
                await _letService.ApproveLetAsync(letId);
                return NoContent();
            }

            return NotFound();
        }
    }
}
