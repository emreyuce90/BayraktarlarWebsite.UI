using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Helpers.Abstract;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class TabelaController : Controller
    {
        private readonly DatabaseConnection _context;
        private readonly IToastNotification _toastNotification;
        private readonly IImageHelper _imageHelper;

        public TabelaController(DatabaseConnection context, IToastNotification toastNotification, IImageHelper imageHelper)
        {
            _context = context;
            _toastNotification = toastNotification;
            _imageHelper = imageHelper;
        }
        [HttpGet]
        public IActionResult Add()
        {
            //müşteri adlarını ve idsini  çek ve customer 
            var customers = _context.Customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name,

            }).ToList();


            var brands = _context.Brands.ToList();
            var materials = _context.Materials.ToList();
            return View(new Talep
            {
                CustomerViewModel = customers,
                Brands = brands,
                Materials = materials
            });


        }
        [HttpPost]
        public async Task<IActionResult> Add(Talep talep)
        {
            if (ModelState.IsValid)
            {
                var tabela = new Tabela();
                //eğer kullanıcı tabela resmi seçtiyse
                //gelen dosya adını kayıt et
                if (talep.Picture != null)
                {
                    var fileName = await _imageHelper.UploadImageAsync("tabelaImages", talep.Picture);
                    tabela.Pictures = fileName;
                }

                tabela.BrandId = talep.BrandId;
                tabela.CreatedDate = DateTime.Now;
                tabela.CustomerId = talep.CustomerId;
                tabela.MaterialId = talep.MaterialId;
                tabela.Notes = talep.Notes;
                tabela.StatusId = 1;
                tabela.UserId = 1;

                _context.Tabelas.Add(tabela);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Tabela ekleme işlemi başarılı");
                return RedirectToAction("Index");


            }
            else
            {
                var customers = _context.Customers.Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    Name = c.Name,

                }).ToList();

                var brands = _context.Brands.ToList();
                var materials = _context.Materials.ToList();
                _toastNotification.AddErrorToastMessage("Bir hata meydana geldi", new ToastrOptions
                {
                    Title = "İşlem Başarılı"
                });
                return View(new Talep
                {
                    CustomerViewModel = customers,
                    Brands = brands,
                    Materials = materials
                });
            }



        }



        [HttpGet]
        public IActionResult Index()
        {
            //databaseden tüm tabelaları çekelim
            var tabelalar = _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Status).Include(t => t.Customer).ToList();

            //Liste olarak TabelaViewModel nesnesi  oluşturalım
            List<TabelaViewModel> model = new List<TabelaViewModel>();

            //databasedeki verileri gezelim ve boş olan modelimize ekleyelim
            foreach (var talep in tabelalar)
            {
                var t = new TabelaViewModel
                {
                    BrandName = talep.Brand.Name,
                    CreatedDate = talep.CreatedDate,
                    CustomerName = talep.Customer.Name,
                    MaterialName = talep.Material.Name,
                    Notes = talep.Notes,
                    SmallThumbnail = talep.Pictures,
                    StatusName = talep.Status.Name
                };
                model.Add(t);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int tabelaId)
        {
            //parametre ile gelen idye ait tabelayı bul
            var tabela = await _context.Tabelas.FirstOrDefaultAsync(t => t.Id == tabelaId);
            return View();
        }
    }
}
