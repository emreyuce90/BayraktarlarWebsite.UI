using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class HunterController : Controller
    {
        private readonly IHunterService _hunterService;
        private readonly IToastNotification _toastNotification;
        public HunterController(IHunterService hunterService, IToastNotification toastNotification)
        {
            _hunterService = hunterService;
            _toastNotification = toastNotification;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hunters = await _hunterService.GetAll();
           
            return View(hunters);
        }

        [HttpGet]
        public async Task<IActionResult> AddHunter()
        {
            return View(new Hunter());
        }

        [HttpPost]
        public async Task<IActionResult> AddHunter(Hunter hunter)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Ekleme işlemi başarısız", new ToastrOptions
                {
                    Title = "Hata"
                });
                return View(hunter);
            }
            hunter.CreatedDate = DateTime.Now;
            await _hunterService.AddAsync(hunter);
            _toastNotification.AddSuccessToastMessage("Ekleme işlemi başarılı", new ToastrOptions
            {
                Title = "İşlem Başarılı"
            });
            return RedirectToAction("Index");
        }
    }
}
