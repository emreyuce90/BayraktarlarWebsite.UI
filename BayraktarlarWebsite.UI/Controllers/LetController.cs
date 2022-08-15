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
        private readonly IMailService _mailService;
        private readonly INotificationService _notificationService;
        private readonly ILetTimeCalculator _calculate;
        private readonly UserManager<User> _userManager;
        private readonly ILetService _letService;
        private readonly IToastNotification _toastNotification;
        public LetController(ILetService letService, IToastNotification toastNotification, UserManager<User> userManager, ILetTimeCalculator calculate, INotificationService notificationService, IMailService mailService)
        {
            _letService = letService;
            _toastNotification = toastNotification;
            _userManager = userManager;
            _calculate = calculate;
            _notificationService = notificationService;
            _mailService = mailService;
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
                    //Mail gönderimi
                    var emailToSend = new EmailSendDto
                    {
                        UserEmailAddress = loggedInUser.Email,
                        Description =$"Sayın{loggedInUser.FirstName+" "+loggedInUser.LastName} izin isteme işleminiz başarıyla yöneticinize iletilmiştir,iznin onaylanması durumunda tarafınıza e-mail ve websitesi üzerinden bildirim yapılacaktır",
                        Subject = "İzin isteme işlemi başarılı"
                    };
                    _mailService.SendMail(emailToSend);
                    //Bildirim
                    var notification = new NotificationAddDto
                    {
                        Name="İzin talebiniz oluşturuldu",
                        Description =$"{letAddDto.StartDate.ToShortDateString()} - {letAddDto.EndDate.ToShortDateString()} tarihleri arası {letAddDto.DayCount} günlük izin talebiniz kaydedilmiştir.",
                        UserId= loggedInUser.Id,
                        CreatedDate = DateTime.Now,
                        IsRead = false,
                        RememberDate=DateTime.Now                        
                    };
                    var notification2 = new NotificationAddDto
                    {
                        Name = "İzin talebi oluşturuldu",
                        Description = $"{loggedInUser.UserName} adlı kullanıcı {letAddDto.DayCount} günlük izin talebinde bulundu.",
                        UserId = 1,
                        CreatedDate = DateTime.Now,
                        IsRead =false,
                        RememberDate = DateTime.Now
                    };
                    await _notificationService.AddNotificationAsync(notification);
                    await _notificationService.AddNotificationAsync(notification2);
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
                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                //Getting let
                var let =await _letService.GetAsync(letId);
                await _letService.ApproveLetAsync(letId);
                //Mail gönderme işlemi
                var emailToSend = new EmailSendDto
                {
                    Description=$"Tebrikler,izniniz onaylandı\n.İzin başlangıç tarihiniz: {let.Let.StartDate} \n izin bitiş tarihiniz: {let.Let.EndDate}\n Gün: {let.Let.DayCount}",
                    Subject="Tebrikler! İzniniz onaylandı",
                    UserEmailAddress =let.Let.User.Email
                };
                _mailService.SendMail(emailToSend);
                //Bildirim
                var notification = new NotificationAddDto
                {
                    Name = "İzin talebiniz onaylandı",
                    Description = $"{let.Let.StartDate.ToShortDateString()} ile {let.Let.EndDate.ToShortDateString()} tarihleri arasında {let.Let.DayCount} günlük izniniz {let.Let.ApprovedDate.ToShortDateString()} tarhinde {loggedInUser.UserName} kullanıcısı tarafından onaylanmıştır",
                    UserId = let.Let.UserId,
                    CreatedDate = DateTime.Now,
                    RememberDate = DateTime.Now
                };
                await _notificationService.AddNotificationAsync(notification);
                return NoContent();
            }

            return NotFound();
        }
    }
}
