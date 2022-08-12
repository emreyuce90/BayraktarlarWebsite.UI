using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class TicketController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IUrgencyService _urgencyService;
        private readonly UserManager<User> _userManager;
        private readonly ITicketService _ticketService;
        private readonly INotificationService _notificationService;
        public TicketController(ITicketService ticketService, UserManager<User> userManager, IUrgencyService urgencyService, INotificationService notificationService, IMailService mailService)
        {
            _ticketService = ticketService;
            _userManager = userManager;
            _urgencyService = urgencyService;
            _notificationService = notificationService;
            _mailService = mailService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await _ticketService.GetAllAsync(loggedInUser.Id));
        }

        [HttpGet]
        public async Task<IActionResult> Remainder()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await _ticketService.GetAllFutureAsync(loggedInUser.Id));
        }

        [HttpGet]
        public async Task<IActionResult> ClosedTickets()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await _ticketService.ClosedTicketsAsync(loggedInUser.Id));
        }

        [HttpGet]
        public async Task<IActionResult> AddTicket()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            //Aciliyet tablosunu çek
            var aciliyetler = await _urgencyService.GetAllAsync();
            ViewBag.Select = new SelectList(aciliyetler.Urgencies, "Id", "UrgencyName");
            //Oturum açan kullanıcı admin mi?
            bool isAdmin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");
            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
                //Tüm kullanıcıları çek
                var allUsers = await _userManager.Users.ToListAsync();
                var selectList = new SelectList(allUsers, "Id", "UserName");
                ViewBag.Users = selectList;
            }
            else
            {
                ViewBag.IsAdmin = false;
            }
            return View(new TicketAddDto() { RemainderDate = Convert.ToDateTime(DateTime.Now.ToString("f")) });
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketAddDto model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                model.CreatedDate = DateTime.Now;
                bool isAdmin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");
                if (isAdmin)
                {
                    model.IsAssigned = true;
                }
                else
                {
                    model.UserId = loggedInUser.Id;
                }
                await _ticketService.AddTicketAsync(model);
                if (model.RemainderDate > DateTime.Now && isAdmin == false)
                {
                    //Hatırlatıcılı görev //Kullanıcı
                    var notification = new NotificationAddDto
                    {
                        CreatedDate = DateTime.Now,
                        RememberDate = model.RemainderDate,
                        Description = $"{model.Subject} konulu görevinizi unutmayınız",
                        IsRead = false,
                        Name = $"Hatırlatıcı!",
                        UserId = loggedInUser.Id
                    };
                    await _notificationService.AddNotificationAsync(notification);
                    var email = new EmailSendDto
                    {
                        UserEmailAddress = loggedInUser.Email,
                        Subject = "Hatırlatıcılı Görev Oluşturuldu",
                        Description = $"Görev başlığınız:{model.Subject}/n Görev Detayınız: {model.Detail}/n Görev Oluşturma Tarihiniz: {model.CreatedDate}/n Görevi tamamlamanız gereken tarih: {model.RemainderDate}/n Not:Görev hatırlatıcınızı websitesi üzerinden bildirim olarak alabilirsiniz,görev hatırlatma tarihinde ayrıca email gönderilmeyecektir/n Bayraktarlar Akaryakıt Petrol Ürünleri"
                    };
                    _mailService.SendMail(email);
                }
                //Hatırlatıcılı bildirim veya oluşturan kişi admin ise
                else if (model.RemainderDate > DateTime.Now || isAdmin == true)
                {
                    var notification = new NotificationAddDto
                    {
                        CreatedDate = DateTime.Now,
                        RememberDate = model.RemainderDate,
                        Description = $"{model.Subject} konulu görevinizi yapmayı unutmayınız",
                        IsRead = false,
                        Name = $"Admin size bir görev atadı!",
                        UserId = model.UserId
                    };
                    await _notificationService.AddNotificationAsync(notification);
                    var email = new EmailSendDto
                    {
                        UserEmailAddress = loggedInUser.Email,
                        Subject = "Yöneticiniz size bir görev atadı!",
                        Description = $"Görev başlığınız:{model.Subject}/n Görev Detayınız: {model.Detail}/n Görev Oluşturma Tarihiniz: {model.CreatedDate}/n Görevi tamamlamanız gereken tarih: {model.RemainderDate}/n Not:Görev hatırlatıcınızı websitesi üzerinden bildirim olarak alabilirsiniz,görev hatırlatma tarihinde ayrıca email gönderilmeyecektir/n Bayraktarlar Akaryakıt Petrol Ürünleri"
                    };
                    _mailService.SendMail(email);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveTicket(int ticketId)
        {
            if (ticketId != 0)
            {
                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                await _ticketService.ApproveAsync(ticketId);
                //Bilgilendirme yap
                var notification = new NotificationAddDto
                {
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                    Description = $"{loggedInUser.UserName} adlı kullanıcı atadığınız görevi tamamladı",
                    RememberDate = DateTime.Now,
                    Name = $"Atadığınız görevi tamamlandı",
                    UserId = 1
                };
                await _notificationService.AddNotificationAsync(notification);

                return NoContent();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public async Task<IActionResult> UpdateTicket(int ticketId)
        {
            var urgencies = await _urgencyService.GetAllAsync();
            var selectList = new SelectList(urgencies.Urgencies, "Id", "UrgencyName");
            ViewBag.Select = selectList;
            var updatedTicket = await _ticketService.GetAsync(ticketId);
            return View(updatedTicket);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTicket(TicketUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                model.UserId = loggedInUser.Id;
                await _ticketService.UpdateTicketAsync(model);
                if (model.RemainderDate > DateTime.Now)
                {
                    var notification = new NotificationAddDto
                    {
                        CreatedDate = DateTime.Now,
                        RememberDate = model.RemainderDate,
                        Description = $"{model.Subject} konulu görevinizi unutmayınız",
                        IsRead = false,
                        Name = $"Hatırlatıcı!",
                        UserId = loggedInUser.Id
                    };
                    await _notificationService.AddNotificationAsync(notification);
                }
                else
                {

                }
                return RedirectToAction("Index");
            }
            var urgencies = await _urgencyService.GetAllAsync();
            var selectList = new SelectList(urgencies.Urgencies, "Id", "UrgencyName");
            ViewBag.Select = selectList;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Assignment()
        {
            var model = await _ticketService.AssignedTicketsAsync();
            return View(model);
        }
    }
}
