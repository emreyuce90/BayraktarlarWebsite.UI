using AutoMapper;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class LetController : Controller
    {
        private readonly ILetService _letService;
        private readonly IToastNotification _toastNotification;
        public LetController(ILetService letService, IToastNotification toastNotification)
        {
            _letService = letService;
            _toastNotification = toastNotification;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _letService.GetAllAsync());
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
                await _letService.AddLetAsync(letAddDto);
                _toastNotification.AddSuccessToastMessage("İzin talebi oluşturma işlemi başarılı",new ToastrOptions
                {
                    Title ="İşlem Başarılı"
                });
                return RedirectToAction("Index");
            }
            return View(letAddDto);
        }
    }
}
