using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class HunterController : Controller
    {
        private readonly IDistrictService _districtService;
        private readonly ITownService _townService;
        private readonly IHunterService _hunterService;
        private readonly IToastNotification _toastNotification;
        private readonly DatabaseConnection _context;
        public HunterController(IHunterService hunterService, IToastNotification toastNotification, DatabaseConnection context, ITownService provinceService, IDistrictService districtService, ITownService townService)
        {
            _hunterService = hunterService;
            _toastNotification = toastNotification;
            _districtService = districtService;
            _townService = townService;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hunters = await _context.Hunters.Include(h=>h.Town).Include(h=>h.District).Include(h=>h.HunterProducts).ThenInclude(hp=>hp.Product).ThenInclude(sb=>sb.SubProducts).ToListAsync();
            //var hunters = await _hunterService.GetAll();
           
            return View(hunters);
        }

        [HttpGet]
        public async Task<IActionResult> AddHunter()
        {
            var town = await _townService.GetAllAsync();
            var ds = await _districtService.GetAllAsync();
            var pd = await _context.Products.ToListAsync();
            return View(new HunterAddDto() { Towns = town, Districts = ds });
        }

        [HttpPost]
        public async Task<IActionResult> AddHunter(HunterAddDto hunter)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Ekleme işlemi başarısız", new ToastrOptions
                {
                    Title = "Hata"
                });
                return View(hunter);
            }
            var hunterProductList = new List<HunterProduct>();

            if (hunter.Battery == true)
            {
                var b =new HunterProduct { ProductId = 3 };
                hunterProductList.Add(b);   
            }
            if (hunter.Oil == true)
            {
                var bo = new HunterProduct { ProductId = 1 };
                hunterProductList.Add(bo);

            }
            if (hunter.Filter == true)
            {
                var f = new HunterProduct { ProductId = 2 };
                hunterProductList.Add(f);

            }

            var h = new Hunter();
            h.CreatedDate = DateTime.Now;
            h.Adres = hunter.Adres;
            h.CepTelefonu=hunter.CepTelefonu;
            h.WasteBarrel=hunter.WasteBarrel;
            h.TabelaAdi=hunter.TabelaAdi;   
            h.ConsumptionPerYear=hunter.ConsumptionPerYear;
            h.Description=hunter.Description;
            h.DistrictId=hunter.DistrictId;
            h.TownId=hunter.TownId;
            h.SanayiSitesiAdi = hunter.SanayiSitesiAdi;
            h.Not = hunter.Not;
            h.UstaAdi = hunter.UstaAdi;
            h.HunterProducts=hunterProductList;
            try
            {
                await _hunterService.AddAsync(h);
                _toastNotification.AddSuccessToastMessage("Ekleme işlemi başarılı", new ToastrOptions
                {
                    Title = "İşlem Başarılı"
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"{ex.Message}", new ToastrOptions
                {
                    Title = "Hata"
                });
                return View(hunter);

            }
          
        }

        [HttpGet]
        public async Task<JsonResult> GetDistrictsByTownId(int townId)
        {
            var districts = await _districtService.GetByTownsIdAsync(townId);
            return Json(districts);
        }

        [HttpGet]
        public async Task<JsonResult> GetSubProductsByProductId(int productId)
        {
            var subProducts = await _context.SubProducts.Where(s=>s.ProductId == productId).ToListAsync();  
            return Json(subProducts);
        }
    }
}
